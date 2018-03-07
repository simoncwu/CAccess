using System;
using System.Globalization;
namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Enumeration for the different kinds of delivery methods.
	/// </summary>
	public enum DeliveryType : byte
	{
		/// <summary>
		/// Default unknown delivery type
		/// </summary>
		Unknown,
		/// <summary>
		/// Delivery via e-mail message.
		/// </summary>
		Email, //EMAIL
		/// <summary>
		/// Delivery via fax transmission.
		/// </summary>
		Fax, //FAX
		/// <summary>
		/// Delivery in person by hand.
		/// </summary>
		Hand, //HAND
		/// <summary>
		/// Delivery via internet.
		/// </summary>
		Internet, //INT
		/// <summary>
		/// Delivery via postal mail.
		/// </summary>
		Mail, //MAIL
		/// <summary>
		/// Delivery via messenger/courier.
		/// </summary>
		Messenger, //MESS
		/// <summary>
		/// Delivery via registered or certified postal mail.
		/// </summary>
		RegisteredCertified, //REG
		/// <summary>
		/// Delivery via Federal Express.
		/// </summary>
		FedEx //FEDEX
	}

	partial class CPConvert
	{
		/// <summary>
		/// Converts the value of the specified <see cref="DeliveryType"/> to its equivalent <see cref="String"/> representation.
		/// </summary>
		/// <param name="deliveryType">A delivery type.</param>
		/// <returns>The <see cref="String"/> equivalent of the value of <paramref name="deliveryType"/>.</returns>
		public static string ToString(DeliveryType deliveryType)
		{
			switch (deliveryType)
			{
				case DeliveryType.Email: return "E-mail";
				case DeliveryType.Fax: return "Fax";
				case DeliveryType.Hand: return "Hand";
				case DeliveryType.Internet: return "Internet";
				case DeliveryType.Mail: return "Mail";
				case DeliveryType.Messenger: return "Messenger";
				case DeliveryType.RegisteredCertified: return "Registered/Certified Mail";
				case DeliveryType.FedEx: return "Federal Express";
				default: return string.Empty;
			}
		}

		/// <summary>
		/// Parses and converts the specified code to its equivalent <see cref="DeliveryType"/> representation.
		/// </summary>
		/// <param name="code">A delivery code.</param>
		/// <returns>The <see cref="DeliveryType"/> equivalent of <paramref name="code"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="code"/> is a null reference.</exception>
		public static DeliveryType ToDeliveryType(string code)
		{
			if (code == null) throw new ArgumentNullException("code");
			switch (code.ToUpper(CultureInfo.InvariantCulture))
			{
				case "EMAIL": return DeliveryType.Email;
				case "FAX": return DeliveryType.Fax;
				case "HAND": return DeliveryType.Hand;
				case "INT": return DeliveryType.Internet;
				case "MAIL": return DeliveryType.Mail;
				case "MESS": return DeliveryType.Messenger;
				case "REG": return DeliveryType.RegisteredCertified;
				default: return DeliveryType.Unknown;
			}
		}
	}
}
