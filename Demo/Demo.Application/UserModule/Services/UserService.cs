using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Application.Contract.DTOs;
using Demo.Application.UserModule.Adapter;
using Demo.Domain.UserModule.Aggregates;
using Demo.Framework.Abstract;

namespace Demo.Application.UserModule.Services
{
    /// <summary>
    /// User service class
    /// contains services related to user
    /// </summary>
    public class UserService : IUserService
    {
        #region Private Variables

        private readonly IUserRepository _userRepository;
        private readonly IQueryableUnitOfWork _queryableUnitOfWork;

        #endregion

        #region Constructor

        public UserService(IUserRepository userRepository, IQueryableUnitOfWork queryableUnitOfWork)
        {
            if (null == userRepository)
            {
                throw new ArgumentNullException("userRepository");
            }
            if (null == queryableUnitOfWork)
            {
                throw new ArgumentNullException("queryableUnitOfWork");
            }

            _userRepository = userRepository;
            _queryableUnitOfWork = queryableUnitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO GetUserById(int id)
        {
            using (_queryableUnitOfWork)
            {
                // 1. Pass value to repository and use linq specifications
                //var user = _userRepository.GetUserById(1);

                // 2. create specifications in service and use
                var specification = UserSpecifications.UserIdEquals(id);

                var user2 = _userRepository.AllMatching(specification).FirstOrDefault();

                return UserAdapter.AdaptToUserDto(user2);
            }
        }

        /// <summary>
        /// get all user list
        /// </summary>
        /// <returns></returns>
        public IList<UserDTO> GetAll()
        {
            using (_queryableUnitOfWork)
            {
                var userList = _userRepository.GetAll().ToList();

                return userList.Any() ? UserAdapter.AdaptToUserDtoList(userList) : null;
            }
        }

        /// <summary>
        /// insert new user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public bool InsertUser(UserDTO userDto)
        {
            var user = UserAdapter.AdaptToUser(userDto);

            using (_queryableUnitOfWork)
            {
                _userRepository.Add(user);

                _queryableUnitOfWork.Commit();
            }

            return true;
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public bool UpdateUser(UserDTO userDto)
        {
            var user = UserAdapter.AdaptToUser(userDto);

            using ((_queryableUnitOfWork))
            {
                _userRepository.Modify(user);

                _queryableUnitOfWork.Commit();
            }
            return true;
        }

        /// <summary>
        /// Delete User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(int id)
        {
            using (_queryableUnitOfWork)
            {
                var user = _userRepository.GetUserById(id);

                if (user == null)
                {
                    return false;
                }
                using ((_queryableUnitOfWork))
                {
                    _userRepository.Remove(user);

                    _queryableUnitOfWork.Commit();

                    return true;
                }
            }
        }

        #endregion
    }
}
