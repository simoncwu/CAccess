using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Security;
using System.ServiceModel;
using Cfb.Cfis.CampaignContacts;
using Cfb.Cfis.Data;
using Cfb.Cfis.Data.AuthorizedCommitteeTdsTableAdapters;

namespace Cfb.CandidatePortal.ServiceModel.CPDataService
{
    public partial interface IDataService
    {
        /// <summary>
        /// Retrieves all campaign liaisons on record for the specified candidate and committee.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose liaisons are to be retrieved.</param>
        /// <param name="committeeID">The ID of the committee whose liaisons are to be retrieved.</param>
        /// <returns>A collection of all campaign liaisons on record for the specified candidate and committee, indexed by liaison ID.</returns>
        [OperationContract(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        [FaultContract(typeof(OfflineDataException))]
        Dictionary<byte, Liaison> GetLiaisons(string candidateID, char committeeID);
    }

    public partial class DataService : IDataService
    {
        /// <summary>
        /// Retrieves all campaign liaisons on record for the specified candidate and committee.
        /// </summary>
        /// <param name="candidateID">The ID of the candidate whose liaisons are to be retrieved.</param>
        /// <param name="committeeID">The ID of the committee whose liaisons are to be retrieved.</param>
        /// <returns>A collection of all campaign liaisons on record for the specified candidate and committee, indexed by liaison ID.</returns>
        public Dictionary<byte, Liaison> GetLiaisons(string candidateID, char committeeID)
        {
            using (AuthorizedCommitteeTds ds = new AuthorizedCommitteeTds())
            {
                using (CampaignLiaisonsTableAdapter ta = new CampaignLiaisonsTableAdapter())
                {
                    ta.Fill(ds.CampaignLiaisons, candidateID, committeeID.ToString());
                }
                Dictionary<byte, Liaison> d = new Dictionary<byte, Liaison>(ds.CampaignLiaisons.Count);
                foreach (AuthorizedCommitteeTds.CampaignLiaisonsRow row in ds.CampaignLiaisons.Rows)
                {
                    byte id;
                    if (byte.TryParse(row.LiaisonID, out id))
                    {
                        d.Add(id, new Liaison(id)
                        {
                            Type = CPConvert.ToLiaisonType(row.LiaisonTypeCode.Trim()) == LiaisonType.Consultant ? EntityType.Consultant : EntityType.Liaison,
                            LiaisonType = CPConvert.ToLiaisonType(row.LiaisonTypeCode.Trim()),
                            ContactOrder = CPConvert.ToContactOrder(row.ContactOrderCode),
                            Honorific = CPConvert.ToHonorific(row.HonorificCode.Trim()),
                            LastName = row.LastName.Trim(),
                            FirstName = row.FirstName.Trim(),
                            MiddleInitial = string.IsNullOrWhiteSpace(row.MI) ? null : row.MI.Trim().ToCharArray()[0] as char?,
                            Address = new PostalAddress()
                            {
                                StreetNumber = row.StreetNumber.Trim(),
                                StreetName = row.StreetName.Trim(),
                                Apartment = row.Apartment.Trim(),
                                City = row.City.Trim(),
                                State = row.State.Trim(),
                                Zip = new ZipCode(row.Zip.Trim())
                            },
                            DaytimePhone = new PhoneNumber(row.DaytimePhone.Trim())
                            {
                                Extension = row.DaytimePhoneExt.Trim()
                            },
                            EveningPhone = new PhoneNumber(row.EveningPhone.Trim())
                            {
                                Extension = row.EveningPhoneExt.Trim()
                            },
                            Fax = new PhoneNumber(row.Fax.Trim()),
                            Email = row.Email.Trim(),
                            EntityName = row.EntityName.Trim(),
                            HasManagerialControl = "Y".Equals(row.HasManagerialControl.Trim(), System.StringComparison.OrdinalIgnoreCase),
                            IsVGLiaison = "Y".Equals(row.IsVGLiaison.Trim(), System.StringComparison.OrdinalIgnoreCase)
                        });
                    }
                }
                return d;
            }
        }
    }
}
