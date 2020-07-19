using System.Web;
using Demo.Application.UserModule.Services;
using Demo.Domain.UserModule.Aggregates;
using Demo.Framework.Abstract;
using Demo.Framework.Concrete;
using Demo.Infrastructure.Context;
using Demo.Infrastructure.Repositories;
using Microsoft.Practices.Unity;

namespace Demo.DependencyResolver
{
    /// <summary>
    /// Unity configuration resolver
    /// </summary>
    public class UnityConfigResolver
    {
        public static void RegisterComponents(IUnityContainer container)
        {
            if (HttpContext.Current != null)
            {
                container.RegisterType<IDatabaseContext, DatabaseContext>(new PerHttpRequestLifetimeManager());
            }
            else
            {
                container.RegisterType<IDatabaseContext, DatabaseContext>(new PerHttpRequestLifetimeManager());
            }

            container.RegisterType<IQueryableUnitOfWork, QueryableUnitOfWork>();

            // Application Services
            container.RegisterType<IUserService, UserService>();

            // Domain Services
            container.RegisterType<Domain.UserModule.Services.IUserService, Domain.UserModule.Services.UserService>();

            // Repositoris
            container.RegisterType<IUserRepository, UserRepository>();


        }
    }
}
