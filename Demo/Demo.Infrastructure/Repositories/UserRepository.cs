using System.Linq;
using Demo.Domain.UserModule.Aggregates;
using Demo.Framework.Abstract;
using Demo.Framework.Concrete;

namespace Demo.Infrastructure.Repositories
{
    /// <summary>
    /// user repository class
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IQueryableUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            return Querayable().FirstOrDefault(user => user.ID == id);
        }
    }
}
