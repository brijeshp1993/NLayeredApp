using Demo.Framework.Abstract;
using Demo.Framework.Concrete;
using Demo.Framework.Specifications;

namespace Demo.Domain.UserModule.Aggregates
{
    /// <summary>
    /// user specification class
    /// </summary>
    public class UserSpecifications
    {
        /// <summary>
        /// user id equals specification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ISpecification<User> UserIdEquals(int id)
        {
            Specification<User> specification = new TrueSpecification<User>();
            return specification &= new DirectSpecification<User>(user=>user.ID.Equals(id));
        } 
    }
}
