using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Cfb.CandidatePortal.Security
{
    /// <summary>
    /// Represents the results of an update operation on a <see cref="CPUser"/> object.
    /// </summary>
    [DataContract]
    public struct UpdateResult
    {
        /// <summary>
        /// The updated user.
        /// </summary>
        [DataMember(Name = "User")]
        private readonly CPUser _user;

        /// <summary>
        /// Gets the updated user.
        /// </summary>
        public CPUser User
        {
            get { return _user; }
        }

        /// <summary>
        /// The result state of the update.
        /// </summary>
        [DataMember(Name = "State")]
        private readonly UpdateResultState _state;

        /// <summary>
        /// Gets the result state of the update.
        /// </summary>
        public UpdateResultState State
        {
            get { return _state; }
        }

        /// <summary>
        /// Gets whether or not the update succeeded without error.
        /// </summary>
        public bool Succeeded
        {
            get
            {
                return _state == UpdateResultState.Success
                    || (_state & UpdateResultState.EmailAddressChanged) == UpdateResultState.EmailAddressChanged
                    || (_state & UpdateResultState.DisplayNameChanged) == UpdateResultState.DisplayNameChanged;
            }
        }

        /// <summary>
        /// Creates a new <see cref="UpdateResult"/> structure that contains the specified results of a user update operation.
        /// </summary>
        /// <param name="user">The updated user.</param>
        /// <param name="state">The result state of the update.</param>
        public UpdateResult(CPUser user, UpdateResultState state)
        {
            _user = user;
            _state = state;
        }
    }
}
