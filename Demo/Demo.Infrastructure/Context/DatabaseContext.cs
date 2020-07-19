using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Demo.Framework.Abstract;
using Demo.Infrastructure.Context.TypeConfigurations;

namespace Demo.Infrastructure.Context
{
    /// <summary>
    /// Database Context class
    /// </summary>
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext()
            : base("name=DefaultConnection")
        {
            //Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseAlways<DatabaseContext>());
            //Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
            Database.SetInitializer<DatabaseContext>(new NullDatabaseInitializer<DatabaseContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove unused conventions
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Add entity configurations in a structured way using 'TypeConfiguration’ classes
            modelBuilder.Configurations.Add(new UserTypeConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
