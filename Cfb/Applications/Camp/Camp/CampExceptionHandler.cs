using System;
using System.Net.Mail;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Cfb.CandidatePortal;

namespace Cfb.Camp
{
	/// <summary>
	/// Provides application-wide exception handling for the C-Access Management Program.
	/// </summary>
	internal static class CampExceptionHandler
	{
		/// <summary>
		/// Represents the method that will handle the <see cref="Application.ThreadException"/> event of an <see cref="Application"/>. 
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="ThreadExceptionEventArgs"/> that contains the event data.</param>
		public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			// Code that runs when an unhandled error occurs
			// check for SQL connection problems and show offline message as appropriate
			Exception ex = e.Exception;
			if (CPProviders.DataProvider.IsOfflineDataException(ex))
			{
				MessageBox.Show("Your last transaction could not be completed because a web transfer is currently in progress.\n\nPlease close any open windows and try again later.", "Error: Web Transfer In Progress", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				CampProgram.CampForm.UseWaitCursor = false;
				return;
			}
			try
			{
				// send auto-email
				MailMessage message = new MailMessage();
				message.To.Add("caccess@nyccfb.info");
				message.Subject = Properties.Settings.Default.CampTitle + " Error Report";
				message.Body = string.Format("User: {0}\n\nMessage: {1}\nSource: {2}\nStack Trace: {3}",
					WindowsIdentity.GetCurrent().Name,
					ex.Message,
					ex.Source,
					ex.StackTrace);
				new SmtpClient().Send(message);
			}
			catch
			{
			}
			MessageBox.Show(string.Format("{0}\n\nAdditional information:\n{1} {2}", ex.Message, ex.GetType().ToString(), ex.StackTrace), Properties.Settings.Default.CampTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
