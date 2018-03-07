using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Cfb.Extensions
{
	/// <summary>
	/// Extensions to classes in the System namespace.
	/// </summary>
	public static class Security
	{
		/// <summary>
		/// Decrypts and retrieves the value of the confidential text.
		/// </summary>
		/// <param name="secureText">The encrypted text to decrypt.</param>
		/// <returns>The decrypted value of the confidential text.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="secureText"/> is null.</exception>
		public static string Decrypt(this SecureString secureText)
		{
			if (secureText == null)
				throw new ArgumentNullException("secureText", "Text must not be null.");
			// read password characters from protected memory into a BSTR
			IntPtr passwordBSTR = default(IntPtr);
			passwordBSTR = Marshal.SecureStringToBSTR(secureText);
			return Marshal.PtrToStringBSTR(passwordBSTR);
		}
	}
}
