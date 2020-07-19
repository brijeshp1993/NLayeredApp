using System;
using Demo.Framework;

namespace Demo.Domain.UserModule.Aggregates
{
    /// <summary>
    /// Entity User
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// user id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// email id
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// first name of user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// last name of user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// created on date
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// modified on date
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// is deleted ?
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
