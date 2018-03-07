using System;
using System.Diagnostics;
using Cfb.ExceptionHandling.Properties;
using Cfb.Extensions;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Data.SqlClient;

namespace Cfb.ExceptionHandling
{
	/// <summary>
	/// Represents the CFB exception handling policy.
	/// </summary>
	public static class CfbExceptionPolicy
	{
		private const string ExceptionMessageFormat = @"Exception information: 
    Exception type: {0}
    Exception message: {1}

    Stack trace: {2}";

		private static Settings _settings;

		static CfbExceptionPolicy()
		{
			_settings = Properties.Settings.Default;
		}

		/// <summary>
		/// Logs an exception.
		/// </summary>
		/// <param name="e">The exception to log.</param>
		/// <returns>true if a rethrow is recommended; otherwise, false.</returns>
		public static bool LogException(Exception e)
		{
			SqlException se = e as SqlException;
			if (se != null && !se.IsOfflineException()) // only log non-offline exceptions
				return ExceptionPolicy.HandleException(e, _settings.LogOnlyPolicyName);
			else
				return false;
		}
	}
}
