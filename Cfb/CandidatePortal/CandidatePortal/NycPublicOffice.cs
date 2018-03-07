using System;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object represetnation of a publicly elected governmental office of New York City.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class NycPublicOffice
	{
		/// <summary>
		/// The type of office.
		/// </summary>
		[DataMember(Name = "Type")]
		private readonly NycPublicOfficeType _type;

		/// <summary>
		/// Gets the type of office.
		/// </summary>
		public NycPublicOfficeType Type
		{
			get { return _type; }
		}

		/// <summary>
		/// The borough presided over for the office of Borough President.
		/// </summary>
		[DataMember(Name = "Borough")]
		private NycBorough _borough;

		/// <summary>
		/// Gets the borough presided over if this office is Borough President. A return value indicates whether the fetch succeeded.
		/// </summary>
		/// <param name="borough">When this method returns, contains the borough presided over, if this office is Borough President; otherwise, unknown. This parameter is passed uninitialized.</param>
		/// <returns>true if this office is Borough President and the fetch succeeded; otherwise, false.</returns>
		public bool TryGetBorough(out NycBorough borough)
		{
			borough = this._borough;
			return this._borough != NycBorough.Unknown;
		}

		/// <summary>
		/// Sets the borough presided over for the office of Borough President.
		/// </summary>
		public NycBorough Borough
		{
			set { _borough = value; }
		}

		/// <summary>
		/// The district represented for the office of City Council Member.
		/// </summary>
		[DataMember(Name = "District")]
		private byte _district = byte.MinValue;

		/// <summary>
		/// Gets the district represented if this office is City Council Member. A return value indicates whether the fetch succeeded.
		/// </summary>
		/// <param name="district">When this method returns, contains the district represented, if this office is City Council Member; otherwise, unknown. This parameter is passed uninitialized.</param>
		/// <returns>true if there is a district defined for this office and the fetch succeeded; otherwise, false.</returns>
		public bool TryGetDistrict(out byte district)
		{
			district = this._district;
			return this._district > byte.MinValue;
		}

		/// <summary>
		/// Sets the district represented by this office.
		/// </summary>
		public byte District
		{
			set { this._district = value; }
		}

		/// <summary>
		/// Gets whether or not this office is a Citywide position.
		/// </summary>
		public bool IsCitywide
		{
			get
			{
				switch (_type)
				{
					case NycPublicOfficeType.Mayor:
					case NycPublicOfficeType.Comptroller:
					case NycPublicOfficeType.PublicAdvocate:
						return true;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Initializes an undeclared public office of New York City.
		/// </summary>
		public NycPublicOffice()
		{
			_type = NycPublicOfficeType.Undeclared;
		}

		/// <summary>
		/// Declares a public office of New York City.
		/// </summary>
		/// <param name="type">The office type.</param>
		public NycPublicOffice(NycPublicOfficeType type)
		{
			_type = type;
		}

		/// <summary>
		/// Declares a Borough President of New York City.
		/// </summary>
		/// <param name="borough">The borough presided over.</param>
		public NycPublicOffice(NycBorough borough)
		{
			_type = NycPublicOfficeType.BoroughPresident;
			_borough = borough;
		}

		/// <summary>
		/// Converts the value of the specified <see cref="NycPublicOffice"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <returns>The <see cref="String"/> equivalent of the value of this instance..</returns>
		public override string ToString()
		{
			switch (_type)
			{
				case NycPublicOfficeType.BoroughPresident:
					NycBorough borough;
					if (this.TryGetBorough(out borough))
						return string.Format("{0} {1}", CPConvert.ToString(borough), CPConvert.ToString(_type));
					break;
				case NycPublicOfficeType.CityCouncilMember:
					byte district;
					if (this.TryGetDistrict(out district))
						return string.Format("{0}—District {1}", CPConvert.ToString(_type), district);
					break;
			}
			return CPConvert.ToString(_type);
		}

		/// <summary>
		/// Converts the value of the specified <see cref="NycPublicOffice"/> to an abbreviated <see cref="String"/> representation.
		/// </summary>
		/// <returns>The abbreviated <see cref="String"/> equivalent of the value of this instance.</returns>
		public string ToAbbrevString()
		{
			switch (_type)
			{
				case NycPublicOfficeType.BoroughPresident:
					NycBorough borough;
					if (this.TryGetBorough(out borough))
						return string.Format("{0} BP", CPConvert.ToString(borough));
					break;
				case NycPublicOfficeType.CityCouncilMember:
					return "City Council";
			}
			return CPConvert.ToString(_type);
		}
	}
}
