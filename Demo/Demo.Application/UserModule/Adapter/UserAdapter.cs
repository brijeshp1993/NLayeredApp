using System.Collections.Generic;
using System.Linq;
using Demo.Application.Contract.DTOs;
using Demo.Domain.UserModule.Aggregates;

namespace Demo.Application.UserModule.Adapter
{
    /// <summary>
    /// user adapter class
    /// Adapt object to User entity class and UserDTO class
    /// </summary>
    public static class UserAdapter
    {
        /// <summary>
        /// Adapt to user entity
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public static User AdaptToUser(UserDTO userDto)
        {
            return new User
            {
                ID = userDto.ID,
                EmailId = userDto.EmailId,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Password = userDto.Password
            };
        }

        /// <summary>
        /// adapt to user entity list
        /// </summary>
        /// <param name="userDtos"></param>
        /// <returns></returns>
        public static IList<User> AdaptToUserList(IList<UserDTO> userDtos)
        {
            return userDtos.Select(user => new User
            {
                ID = user.ID,
                EmailId = user.EmailId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password
            }).ToList();
        }

        /// <summary>
        /// adapt to user DTO
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UserDTO AdaptToUserDto(User user)
        {
            return new UserDTO
            {
                ID = user.ID,
                EmailId = user.EmailId,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        /// <summary>
        /// adapt to user DTO list
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static IList<UserDTO> AdaptToUserDtoList(IList<User> users)
        {
            return users.Select(user => new UserDTO
            {
                ID = user.ID,
                EmailId = user.EmailId,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            }).ToList();
        }
    }
}
