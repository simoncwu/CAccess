using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using Cfb.CandidatePortal.CPConfiguration;
using Cfb.CandidatePortal.CPSettings;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.ServiceModel.CPDataClient;
using Cfb.CandidatePortal.ServiceModel.CPSecurityClient;
using Cfb.Cfis.CampaignContacts;
using Cfb.ExceptionHandling;

namespace Cfb.CandidatePortal.ServiceProcess.CPReminderDistributionService
{
    /// <summary>
    /// A Windows service for distributing reminder alerts of upcoming events and deadlines to all C-Access accounts via e-mail.
    /// </summary>
    public partial class ReminderDistributionService : ServiceBase
    {
        /// <summary>
        /// The collection of all C-Access user accounts, grouped by candidate ID.
        /// </summary>
        private Dictionary<string, IEnumerable<CPUser>> _accounts;

        /// <summary>
        /// The timer for regularly checking for events to send.
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Creates a new instance of the <see cref="ReminderDistributionService"/> class.
        /// </summary>
        public ReminderDistributionService()
        {
            InitializeComponent();
            GC.KeepAlive(CPProviders.DataProvider = new CPDataProvider());
            GC.KeepAlive(CPProviders.SettingsProvider = new CPSettingsProvider());
            GC.KeepAlive(CPSecurity.Provider = new SecurityProvider());
        }

        /// <summary>
        /// Retrieves a collection of immediately upcoming Doing Business events that are ready for reminder e-mails.
        /// </summary>
        /// <param name="candidateID">The ID of the targeted candidate.</param>
        /// <param name="electionCycle">The election cycle in which to search.</param>
        /// <param name="lastRetrieved">The lower limit date of events that can be retrieved, non-inclusive.</param>
        /// <returns>A collection of immediately upcoming Doing Business events that are ready for reminder e-mails.</returns>
        private IEnumerable<DbrResponseDeadline> GetDoingBusinessEvents(string candidateID, string electionCycle, DateTime lastRetrieved)
        {
            // process events upcoming within [DoingBusinessReminderDays]
            DateTime upperLimit = DateTime.Today.AddDays(Properties.Settings.Default.DoingBusinessReminderDays);
            // last retrieval would have processed all events within [DoingBusinessReminderDays], so we set lower limit one day later to avoid double-sending
            DateTime lowerLimit = lastRetrieved.Date.AddDays(Properties.Settings.Default.DoingBusinessReminderDays + 1);
            // lower limit must not be earlier than tomorrow (future)
            if (lowerLimit <= DateTime.Today)
                lowerLimit = DateTime.Today.AddDays(Properties.Settings.Default.DoingBusinessReminderDays);
            Elections elections = Elections.GetElections(CPProviders.SettingsProvider.MinimumElectionCycle);
            if (elections != null)
            {
                Election election = elections[electionCycle];
                if (election != null)
                {
                    bool isElectionYear = election.Year == DateTime.Now.Year;
                    return from r in DoingBusinessReviews.GetDoingBusinessReviews(candidateID, electionCycle).ResponseDeadlines
                           where r.StartDate.Date >= lowerLimit && r.StartDate.Date <= upperLimit && isElectionYear ? r.IsElectionYearUpcoming : r.IsUpcoming
                           select r;
                }
            }
            return null;
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            if (_timer != null)
                this.OnStop();
            _timer = new Timer(Properties.Settings.Default.TimerIntervalMinutes * 60000);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Start();
            GC.KeepAlive(_timer);
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // check to see if an iteration is already underway
            if (_accounts != null) return;
            DateTime lastRun = CPProviders.SettingsProvider.LastReminderDistributionDate.Date;
            // only run once each day
            if (lastRun.Date == DateTime.Today)
                return;
            _accounts = new Dictionary<string, IEnumerable<CPUser>>();
            try
            {
                // get all candidates/accounts
                foreach (string cid in Cfis.GetCandidates().Keys)
                {
                    List<CPUser> group = new List<CPUser>();
                    var users = CPSecurity.Provider.GetCampaignUsers(cid, null, false);
                    foreach (var user in users)
                    {
                        if (!user.Enabled)
                            continue;
                        group.Add(user);
                    }
                    _accounts[cid] = group;
                }

                // prepare to collect failed addresses
                NameValueCollection failures = new NameValueCollection();

                // iterate through candidates
                foreach (string cid in _accounts.Keys)
                {
                    // retrieve this candidate's users
                    var group = _accounts[cid];
                    if (group == null || group.Count() == 0)
                        continue;
                    // retrieve this candidate's elections
                    foreach (string election in Elections.GetActiveElectionCycles(cid))
                    {
                        // retrieve events from WCF service
                        //ComplianceVisits cv = _dataProxy.GetComplianceVisits(cid, election);
                        //events.AddItems(cv.UpcomingVisits);
                        //events.AddItems(cv.UpcomingDeadlines);
                        //events.AddItems(_dataProxy.GetStatementReviews(cid, election).UpcomingDeadlines);
                        //events.AddItems(_dataProxy.GetFilingDeadlines(cid, election).Upcoming);
                        var dbEvents = GetDoingBusinessEvents(cid, election, lastRun);

                        // iterate through events
                        foreach (CPCalendarItem ev in dbEvents)
                        {
                            if (ev.StartDate.Date > lastRun)
                            {
                                // send e-mail
                                using (EventReminderMessage message = new EventReminderMessage(cid, election, ev))
                                {
                                    //CfbLogger.Write(new CfbLogEntry(string.Format("Sending e-mail for candidate {0}", cid), CfbLogCategory.Trace, 0, CfbEventID.Informational, TraceEventType.Information, "EventReminderMessage Tick Start", null));
                                    message.Recipients = group;
                                    NameValueCollection failed = message.Send();
                                    if (failed.Count > 0)
                                    {
                                        foreach (string recipient in failed.Keys)
                                        {
                                            failures[recipient] = failed[recipient];
                                        }
                                    }
                                }
                            }
                        }

                        int dbCount = dbEvents.Count();
                        if (dbCount > 0)
                            CfbLogger.Write(new CfbLogEntry(string.Format("Processed {0} upcoming Doing Business Review events for candidate ID: {1}, election cycle: {2}", dbCount, cid, election), CfbLogCategory.Email, 0, CfbEventID.Informational, TraceEventType.Information, "EventReminderMessage Processed Events", null));
                    }
                }

                // log failed addresses
                if (failures.Count > 0)
                {
                    StringBuilder errorMessage = new StringBuilder("The following error(s) occurred while sending reminder e-mails to the following C-Access users:\n\n");
                    foreach (string recipient in failures.Keys)
                    {
                        errorMessage.AppendFormat("{0}: {1}\n\n", recipient, failures[recipient]);
                    }
                    CfbLogger.Write(new CfbLogEntry(errorMessage, CfbLogCategory.Email, 0, CfbEventID.EmailFailure, TraceEventType.Warning, "EventReminderMessage Failure", null));
                }
            }
            catch (Exception ex)
            {
                if (CfbExceptionPolicy.LogException(ex))
                    throw;
            }
            finally
            {
                CPProviders.SettingsProvider.LastReminderDistributionDate = DateTime.Now;
                _accounts = null;
            }
        }

        /// <summary>
        /// Stops the executing service.
        /// </summary>
        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        /// <summary>
        /// Executes when a Pause command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service pauses.
        /// </summary>
        protected override void OnPause()
        {
            _timer.Stop();
        }

        /// <summary>
        /// Runs when a Continue command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service resumes normal functioning after being paused.
        /// </summary>
        protected override void OnContinue()
        {
            _timer.Start();
        }
    }
}
