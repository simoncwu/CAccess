using System;
using System.Runtime.Serialization;
using Cfb.Cfis.CampaignContacts;

namespace Cfb.CandidatePortal
{
    /// <summary>
    /// Business object representation of a bank account.
    /// </summary>
    [DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
    public class BankAccount
    {
        /// <summary>
        /// The bank account identifier.
        /// </summary>
        [DataMember(Name = "ID")]
        private readonly byte _id;

        /// <summary>
        /// Gets the bank account identifier.
        /// </summary>
        public byte ID
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the name of the bank/depository that holds this account.
        /// </summary>
        [DataMember]
        public string BankName { get; set; }

        /// <summary>
        /// Gets or sets the bank account name, if available.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the city where the holding bank/depository is located.
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state where the holding bank/despository is located.
        /// </summary>
        [DataMember]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the ZIP code where the holding bank/depository is located.
        /// </summary>
        [DataMember]
        public ZipCode Zip { get; set; }

        /// <summary>
        /// Gets or sets the nine-digit Routing Transit Number (RTN) that identifies the holding bank/depository.
        /// </summary>
        [DataMember]
        public string Rtn { get; set; }

        /// <summary>
        /// Gets or sets the bank account number.
        /// </summary>
        [DataMember]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the date the bank account was opened.
        /// </summary>
        [DataMember]
        public DateTime? OpeningDate { get; set; }

        /// <summary>
        /// Gets or sets the date the bank account was closed, if applicable.
        /// </summary>
        [DataMember]
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the date of the current balance.
        /// </summary>
        [DataMember]
        public DateTime? CurrentBalanceDate { get; set; }

        /// <summary>
        /// Gets or sets the current balance amount.
        /// </summary>
        [DataMember]
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// Gets or sets the type of bank account.
        /// </summary>
        [DataMember]
        public BankAccountType Type { get; set; }

        /// <summary>
        /// Gets or sets the specification for a non-standard bank account type.
        /// </summary>
        [DataMember]
        public string OtherTypeSpecification { get; set; }

        /// <summary>
        /// Gets or sets the purpose of the bank account.
        /// </summary>
        [DataMember]
        public BankAccountPurpose Purpose { get; set; }

        /// <summary>
        /// Gets or sets the specification for a non-standard bank account purpose.
        /// </summary>
        [DataMember]
        public string OtherPurposeSpecification { get; set; }

        /// <summary>
        /// Gets or sets whether or not the bank account is set up to receive public funds via direct deposit.
        /// </summary>
        [DataMember]
        public bool HasDirectDeposit { get; set; }

        /// <summary>
        /// Creates a new instance of a <see cref="BankAccount"/> object.
        /// </summary>
        /// <param name="id">A bank account identifer.</param>
        internal BankAccount(byte id)
        {
            _id = id;
        }
    }
}
