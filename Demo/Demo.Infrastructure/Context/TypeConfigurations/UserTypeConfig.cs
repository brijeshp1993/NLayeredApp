using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Demo.Domain.UserModule.Aggregates;
using Demo.Framework.Constants;

namespace Demo.Infrastructure.Context.TypeConfigurations
{
    /// <summary>
    /// map column configurations for User entity with Users table in database 
    /// </summary>
    public class UserTypeConfig : EntityTypeConfiguration<User>
    {
        public UserTypeConfig()
        {
            ToTable("Users", "dbo");

            HasKey(user => user.ID);

            Property(user => user.ID)
                .HasColumnName("UserId")
                .IsOptional()
                .HasColumnType(Constants.Int)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(user => user.EmailId)
                .HasColumnName("EmailId")
                .HasColumnType(Constants.VarChar)
                .IsRequired()
                .HasMaxLength(50);

            Property(user => user.Password)
                .HasColumnType(Constants.VarChar)
                .HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(10);

            Property(user => user.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(50);

            Property(user => user.LastName)
                .HasColumnType(Constants.VarChar)
                .HasColumnName("LastName")
                .HasMaxLength(50);

            Property(user => user.CreatedOn)
                .HasColumnType(Constants.DateTime)
                .HasColumnName("CreatedOn")
                .IsOptional()
                .HasColumnType("datetime");

            Property(user => user.ModifiedOn)
                .HasColumnType(Constants.DateTime)
                .HasColumnName("ModifiedOn")
                .IsOptional()
                .HasColumnType("datetime");

            Property(user => user.IsDeleted)
                .HasColumnType(Constants.Bit)
                .HasColumnName("IsDeleted")
                .IsOptional()
                .HasColumnType("bit");

            Ignore(user => user.Id);
        }
    }
}
