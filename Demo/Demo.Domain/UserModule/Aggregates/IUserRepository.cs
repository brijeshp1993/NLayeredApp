using Demo.Framework.Abstract;

namespace Demo.Domain.UserModule.Aggregates
{
    /// <summary>
    /// user repository interface
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(int id);
    }
}
