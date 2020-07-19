using System.Collections.Generic;
using Demo.Application.Contract.DTOs;

namespace Demo.Application.UserModule.Services
{
    /// <summary>
    /// user service interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserDTO GetUserById(int id);

        /// <summary>
        /// get all user list
        /// </summary>
        /// <returns></returns>
        IList<UserDTO> GetAll();

        /// <summary>
        /// insert new user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        bool InsertUser(UserDTO userDto);

        /// <summary>
        /// update user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        bool UpdateUser(UserDTO userDto);

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteUser(int id);
    }
}
