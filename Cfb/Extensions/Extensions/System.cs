using System;
using System.Globalization;
using System.Text.RegularExpressions;
namespace Cfb.Extensions
{
	/// <summary>
	/// Extensions to classes in the System namespace.
	/// </summary>
	public static class System_Extensions
	{
		/// <summary>
		/// Truncates a string to a target maximum length.
		/// </summary>
		/// <param name="str">The string to truncate.</param>
		/// <param name="maxLength">The maximum length of the resulting string.</param>
		/// <returns>The string contained by <paramref name="str"/> truncated length <paramref name="maxLength"/>.</returns>
		/// <remarks>If <paramref name="str"/> is shorter than <paramref name="maxLength"/> then the original string is returned.</remarks>
		public static string Truncate(this string str, int maxLength)
		{
			return (str.Length <= maxLength) ? str : str.Substring(0, maxLength);
		}

		/// <summary>
		/// Trims out all instances of a set of characters from a string.
		/// </summary>
		/// <param name="str">The string to trim.</param>
		/// <param name="trimChars">An array of Unicode characters to remove or a null reference.</param>
		/// <returns>The string that remains after all occurrences of the characters in <paramref name="trimChars"/> have been removed from <paramref name="str"/>.</returns>
		public static string TrimAll(this string str, params char[] trimChars)
		{
			foreach (char c in trimChars)
			{
				str = str.Replace(c.ToString(), string.Empty);
			}
			return str;
		}

		/// <summary>
		/// Converts the value of the current <see cref="DateTime"/> object to its equivalent date string representation.
		/// </summary>
		/// <param name="dt">The <see cref="DateTime"/> object to convert.</param>
		/// <returns>A string that contains the date string representation of the current <see cref="DateTime"/> object.</returns>
		public static string ToDateString(this DateTime dt)
		{
			return dt.ToString("MMMM d, yyyy");
		}

		/// <summary>
		/// Determines whether or not a string is in valid email format.
		/// </summary>
		/// <param name="email">The email string to test.</param>
		/// <returns>true if <paramref name="email"/> is in valid email format; otherwise, false.</returns>
		public static bool IsValidEmail(this string email)
		{
			if (String.IsNullOrWhiteSpace(email))
				return false;

			// Use IdnMapping class to convert Unicode domain names.
			try
			{
				email = Regex.Replace(email, @"(@)(.+)$", DomainMapper);
			}
			catch (ArgumentException)
			{
				return false;
			}

			// Return true if strIn is in valid e-mail format.
			return Regex.IsMatch(email,
				   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
				   RegexOptions.IgnoreCase);
		}

		private static string DomainMapper(Match match)
		{
			// IdnMapping class with default property values.
			IdnMapping idn = new IdnMapping();

			string domainName = match.Groups[2].Value;
			domainName = idn.GetAscii(domainName);
			return match.Groups[1].Value + domainName;
		}
	}
}
