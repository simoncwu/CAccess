using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using Cfb.CandidatePortal.Security;
using Cfb.CandidatePortal.Web.Security;
using Cfb.Extensions;

namespace Cfb.Pumacmd
{
    class Pumacmd
    {
        /// <summary>
        /// Enumeration of the various valid creation modes.
        /// </summary>
        private enum UserOperation : byte
        {
            Unknown,
            Create,
            Update,
            Unlock,
            PasswordReset,
            Disable,
            Delete
        }

        /// <summary>
        /// Usage message
        /// </summary>
        private const string _UsageMessage = @"
Configures properties of a user account for the C-Access candidate portal.

Valid operations:
pumacmd /c /f:<first name> /s:<last name> [/m:<middle>] /e:<email>
        /p:<password> [/n:<name>]
    Creates a new user account with the specified properties.

pumacmd /u:<username> [/p:<password>][/e:<email>]
    Sets the properties of an existing account with the specified values.

pumacmd /l:<username>
    Unlocks a locked-out user (due to too many incorrect login attempts).

pumacmd /r:<username>
    Randomly generates a password for the user specified.

pumacmd /x:<username>
    Toggles the active/inactive status of the user specified.

pumacmd /d:<username>
    Deletes the user account specified.

/u:<username>    The username of the Candidate Portal user whose account is
                 to be modified. If the account does not exist, one will be
                 created. Note that if no password is supplied and the
                 account does not exist, the account will not be created.
                 *MUST NOT CONTAIN SPACES*
/e:<email>       Sets the email address for the specified account.
/n:<name>        Sets the username for a new account.
/f:<first name>  The first name of the account to create.
/s:<last name>   The last name of the account to create.
/m:<middle>      The middle initial of the account to create.
";

        static void Main(string[] args)
        {
            Console.WriteLine("CFB Candidate Portal User Setup");
            Console.WriteLine("Copyright (c) NYC Campaign Finance Board.  All rights reserved.\n");
            if ((args.Length == 0))
            {
                Console.WriteLine(_UsageMessage);
            }
            else
            {
                string username = string.Empty;
                string password = string.Empty;
                string email = string.Empty;
                string cfisId = string.Empty;
                string caId = string.Empty;
                string name = string.Empty;
                string firstName = string.Empty;
                string lastName = string.Empty;
                string middle = string.Empty;
                bool passwordSwitch = false;
                bool emailSwitch = false;
                bool cfisIdSwitch = false;
                bool caIdSwitch = false;
                bool nameSwitch = false;
                bool firstNameSwitch = false;
                bool lastNameSwitch = false;
                bool miSwitch = false;
                UserOperation op = UserOperation.Unknown;

                // detect switches and store arguments
                foreach (string arg in args)
                {
                    string trimmed = arg.Trim();
                    if (trimmed.Length == 2)
                    {
                        switch (trimmed.Substring(0, 2))
                        {
                            case "/c":
                                if (op != UserOperation.Unknown)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                op = UserOperation.Create;
                                break;
                            case "/?":
                            default:
                                Console.WriteLine(_UsageMessage);
                                return;
                        }
                    }
                    else if (trimmed.Length > 2)
                    {
                        switch (trimmed.Substring(0, 3))
                        {
                            case "/u:":
                                if (op != UserOperation.Unknown)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                op = UserOperation.Update;
                                username = trimmed.Substring(3);
                                break;
                            case "/r:":
                                if ((args.Length > 1) || (op != UserOperation.Unknown))
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                op = UserOperation.PasswordReset;
                                username = trimmed.Substring(3);
                                break;
                            case "/d:":
                                if ((args.Length > 1) || (op != UserOperation.Unknown))
                                {
                                    Console.WriteLine(_UsageMessage);
                                }
                                op = UserOperation.Delete;
                                username = trimmed.Substring(3);
                                break;
                            case "/l:":
                                if ((args.Length > 1) || (op != UserOperation.Unknown))
                                {
                                    Console.WriteLine(_UsageMessage);
                                }
                                op = UserOperation.Unlock;
                                username = trimmed.Substring(3);
                                break;
                            case "/x:":
                                if ((args.Length > 1) || (op != UserOperation.Unknown))
                                {
                                    Console.WriteLine(_UsageMessage);
                                }
                                op = UserOperation.Disable;
                                username = trimmed.Substring(3);
                                break;
                            case "/p:":
                                if (passwordSwitch)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                passwordSwitch = true;
                                password = trimmed.Substring(3);
                                break;
                            case "/e:":
                                if (emailSwitch)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                emailSwitch = true;
                                email = trimmed.Substring(3);
                                break;
                            case "/i:":
                                if (cfisIdSwitch)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                cfisIdSwitch = true;
                                cfisId = trimmed.Substring(3);
                                break;
                            case "/a:":
                                if (caIdSwitch)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                caIdSwitch = true;
                                caId = trimmed.Substring(3);
                                break;
                            case "/n:":
                                if (nameSwitch)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                nameSwitch = true;
                                name = trimmed.Substring(3);
                                break;
                            case "/f:":
                                if (firstNameSwitch)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                firstNameSwitch = true;
                                firstName = trimmed.Substring(3);
                                break;
                            case "/s:":
                                if (lastNameSwitch)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                lastNameSwitch = true;
                                lastName = trimmed.Substring(3);
                                break;
                            case "/m:":
                                if (miSwitch)
                                {
                                    Console.WriteLine(_UsageMessage);
                                    return;
                                }
                                miSwitch = true;
                                middle = trimmed.Substring(3);
                                break;
                            default:
                                Console.WriteLine(_UsageMessage);
                                return;
                        }
                    }
                }
                if (op == UserOperation.Unknown)
                {
                    Console.WriteLine(_UsageMessage);
                    return;
                }
                UserManagement.EnableTracing = true;

                // show user account properties
                Console.WriteLine("Got the following:");
                if (op != UserOperation.Create)
                    Console.WriteLine("User Name: {0}", username);
                if (passwordSwitch)
                    Console.WriteLine("Password: {0}", password);
                if (emailSwitch)
                    Console.WriteLine("E-mail: {0}", email);
                if (cfisIdSwitch)
                    Console.WriteLine("CFIS ID: {0}", cfisId);
                if (caIdSwitch)
                    Console.WriteLine("C-Access ID: {0}", caId);
                if (firstNameSwitch)
                    Console.WriteLine("First Name: {0}", firstName);
                if (lastNameSwitch)
                    Console.WriteLine("Last Name: {0}", lastName);
                if (miSwitch)
                    Console.WriteLine("Middle Initial: {0}", middle);

                // dispatch operation
                if (op == UserOperation.Create)
                {
                    // create user
                    MembershipUser user = UserManagement.CreateUser(UserManagement.GenerateUserName(firstName, string.IsNullOrWhiteSpace(middle) ? null : (char?)middle.Trim().ToCharArray()[1], lastName, cfisId), password, email, typeof(Pumacmd).ToString());
                    if (!string.IsNullOrEmpty(name))
                    {
                        CPUser cpuser = CPSecurity.Provider.GetUser(username);
                        cpuser.DisplayName = name;
                        CPSecurity.Provider.UpdateUser(cpuser);
                    }
                    Console.WriteLine((user == null) ? "Error creating user!" : string.Format("\nUser {0} was created.", user.UserName));
                }
                else
                {
                    if (string.IsNullOrEmpty(username))
                    {
                        Console.Write(_UsageMessage);
                        return;
                    }
                    MembershipUser user = Membership.GetUser(username);
                    if (op == UserOperation.PasswordReset)
                    {
                        // reset password for user account
                        string newPassword = UserManagement.ResetPassword(Membership.GetUser(username)).Decrypt();
                        if (!string.IsNullOrEmpty(newPassword))
                            Console.WriteLine("Password successfully reset. New password is: {0}", newPassword);
                        else
                            Console.WriteLine("Password reset failed.");
                    }
                    else if (op == UserOperation.Delete)
                    {
                        // delete user account
                        if (UserManagement.DeleteUser(Membership.GetUser(username)))
                            Console.WriteLine("User {0} deleted successfully.", username);
                        else
                            Console.WriteLine("Error deleting user {0}", username);
                    }
                    else if (op == UserOperation.Unlock)
                    {
                        // unlock user account
                        if (UserManagement.UnlockUser(Membership.GetUser(username)))
                            Console.WriteLine("User {0} unlocked successfully.", username);
                        else
                            Console.WriteLine("Error unlocking user {0}", username);
                    }
                    else if (op == UserOperation.Disable)
                    {
                        // enable/disable user account
                        if (user != null && UserManagement.SetStatus(user, !user.IsApproved))
                        {
                            Console.WriteLine("User {0} {1} successfully.", username, user.IsApproved ? "enabled" : "disabled");
                        }
                        else
                        {
                            Console.WriteLine("Error toggling the active status for user {0}", username);
                        }
                    }
                    else if (op == UserOperation.Update)
                    {
                        string message = null;
                        if (emailSwitch)
                        {
                            user.Email = email;
                            message = string.Format("E-mail address for user {0} was set to: {1}", username, email);
                        }
                        Membership.UpdateUser(user);
                        Console.WriteLine(message);
                    }
                    else
                    {
                        Console.WriteLine(_UsageMessage);
                    }
                }
            }
        }
    }
}