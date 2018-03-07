using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Cfb.Extensions
{
	/// <summary>
	/// Extensions to classes in the System.Data.SqlClient namespace.
	/// </summary>
	public static class System_Data_SqlClient
	{
		/// <summary>
		/// A structure for an SQL connection error class/number pair.
		/// </summary>
		private struct SqlConnectError
		{
			/// <summary>
			/// The class value of the error.
			/// </summary>
			private readonly int _class;

			/// <summary>
			/// Gets the class value of the error.
			/// </summary>
			public int Class
			{
				get { return _class; }
			}

			/// <summary>
			/// The number value of the error.
			/// </summary>
			public readonly int _number;

			/// <summary>
			/// Gets the number value of the error.
			/// </summary>
			public int Number
			{
				get { return _number; }
			}

			/// <summary>
			/// Instantiates a new <see cref="SqlConnectError"/> with a class and number.
			/// </summary>
			/// <param name="c">The class value of the error.</param>
			/// <param name="n">The number value of the error.</param>
			public SqlConnectError(int c, int n)
			{
				_class = c;
				_number = n;
			}
		}

		/// <summary>
		/// SQL error codes indicating failure to connect to CFIS database.
		/// </summary>
		private static readonly SqlConnectError[] SqlConnectErrors = new SqlConnectError[] {
			// Timeout expired.  The timeout period elapsed prior to completion of the operation or the server is not responding.
			new SqlConnectError(11, -2),
			// login failed or access denied
			new SqlConnectError(11, 4060),
			// Cannot open database requested in login 'Cfispub'. Login fails. Login failed for user 'web_guest'.
			new SqlConnectError(14, 18456),
			// A transport-level error has occurred when sending the request to the server. (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by the remote host.)
			new SqlConnectError(20, 10054),
			// A transport-level error has occurred when sending the request to the server. (provider: Shared Memory Provider, error: 0 - No process is on the other end of the pipe.)
			new SqlConnectError(20, 233),
			// An error has occurred while establishing a connection to the server. When connecting to SQL Server 2005, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: TCP Provider, error: 0 - No connection could be made because the target machine actively refused it.)
			new SqlConnectError(20, 10061),
			// An error has occurred while establishing a connection to the server.  When connecting to SQL Server 2005, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server)
			new SqlConnectError(20, 1326)
		};

		/// <summary>
		/// Gets whether or not the SQL exception is a result of an offline database.
		/// </summary>
		/// <param name="e">The <see cref="SqlException"/> to examine.</param>
		/// <returns>true if <paramref name="e"/> was caused by an offline database; otherwise, false.</returns>
		public static bool IsOfflineException(this SqlException e)
		{
			foreach (SqlConnectError error in SqlConnectErrors)
			{
				if (e.Class == error.Class && e.Number == error.Number)
					return true;
			}
			return false;
		}
	}
}
