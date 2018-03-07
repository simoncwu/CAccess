using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Cfb.Extensions;

namespace Cfb.Cfis.Data
{
	/// <summary>
	/// Manages connection strings for access to the CFIS database.
	/// </summary>
	public static class ConnectionManager
	{
		/// <summary>
		/// Represents the method that will interact with the database.
		/// </summary>
		public delegate void DBCommandHandler();

		/// <summary>
		/// Sets the CFIS database connection for the specified table adapter to point to the primary database.
		/// </summary>
		/// <param name="adapter">The table adapter being used to interact with the database.</param>
		/// <exception cref="ArgumentNullException"><paramref name="adapter"/> is null.</exception>
		public static void SetPrimaryConnection(this Component adapter)
		{
			SetConnection(adapter, Properties.Settings.Default.CfispubConnectionString);
		}

		/// <summary>
		/// Sets the CFIS database connection for the specified table adapter to point to the backup database.
		/// </summary>
		/// <param name="adapter">The table adapter being used to interact with the database.</param>
		/// <exception cref="ArgumentNullException"><paramref name="adapter"/> is null.</exception>
		public static void SetBackupConnection(this Component adapter)
		{
			SetConnection(adapter, Properties.Settings.Default.BackupCfispubConnectionString);
		}

		/// <summary>
		/// Sets the CFIS database connection for the specified table adapter to use a specific connection string.
		/// </summary>
		/// <param name="adapter">The table adapter being used to interact with the database.</param>
		/// <param name="connectionString">The connection string to use.</param>
		/// <exception cref="ArgumentNullException"><paramref name="adapter"/> is null.</exception>
		private static void SetConnection(this Component adapter, string connectionString)
		{
			if (adapter == null)
				throw new ArgumentNullException("adapter", "Adapter must not be null.");
			PropertyInfo pi = adapter.GetType().GetProperty("Connection", BindingFlags.Instance | BindingFlags.NonPublic);
			if (pi == null)
				return;
			pi.SetValue(adapter, new SqlConnection(connectionString), null);
		}

		/// <summary>
		/// Executes a database command, using a backup datasource if the primary is unavailable.
		/// </summary>
		/// <param name="adapter">The table adapter being used to interact with the database.</param>
		/// <param name="handler">The delegate that will interact with the database.</param>
		public static void ExecuteWithBackupSource(this Component adapter, DBCommandHandler handler)
		{
			try
			{
				if (handler != null)
					handler();
			}
			catch (SqlException e)
			{
				if (e.IsOfflineException() && adapter != null)
				{
					try
					{
						adapter.SetBackupConnection();
						handler();
					}
					finally
					{
						adapter.SetPrimaryConnection();
					}
				}
			}
		}
	}
}
