using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Domain.UserModule.Aggregates;

namespace Demo.Domain.UserModule.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            _userRepository = userRepository;
        }

    }
}
