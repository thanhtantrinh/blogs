using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class UserRoles
    {
        public const string Admin = "admin";
        public const string Manager = "manager";
        public const string User = "user";
    }

    public enum StatusList
    {
        SUSSECSS, RUNNING, STARTING, ERROR
    }
    public enum StatusEntity
    {
        /// <summary>
        /// Entity have not been activated
        /// </summary>
        Active = 1,
        /// <summary>
        /// Entity has been deleted 
        /// </summary>
        Deleted = 2,
        /// <summary>
        /// Locked out
        /// </summary>
        Locked = 3,
    }
    public enum StatusLogin
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Customer does not exist (email or username)
        /// </summary>
        CustomerNotExist = 2,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// Account have not been activated
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// Customer has been deleted 
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// Customer not registered 
        /// </summary>
        NotRegistered = 6,
        /// <summary>
        /// Locked out
        /// </summary>
        LockedOut = 7,
    }
}
