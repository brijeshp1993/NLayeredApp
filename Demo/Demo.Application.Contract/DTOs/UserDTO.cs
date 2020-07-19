using System.ComponentModel.DataAnnotations;

namespace Demo.Application.Contract.DTOs
{
    /// <summary>
    /// User DTO class
    /// transfer data between presentation layer and application layer
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// user unique id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// user email id
        /// </summary>
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        /// <summary>
        /// password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// user first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// user last name
        /// </summary>
        public string LastName { get; set; }
    }
}
