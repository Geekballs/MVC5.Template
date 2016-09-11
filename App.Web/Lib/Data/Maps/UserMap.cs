using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            #region Table

            ToTable("User", schemaName: "Security");

            #endregion

            #region Keys

            HasKey(u => new { u.UserId });

            #endregion

            #region Relationships

            // Nothing to see here!

            #endregion

            #region Properties

            Property(u => u.UserId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_UserId", 1) { IsUnique = true } })).HasColumnName("UserId").HasColumnOrder(1);
            Property(u => u.UserName).IsRequired().HasMaxLength(100).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_UserName", 2) { IsUnique = true } })).HasColumnName("UserName").HasColumnOrder(2);
            Property(u => u.FirstName).IsRequired().HasMaxLength(100).HasColumnName("FirstName").HasColumnOrder(3);
            Property(u => u.LastName).IsRequired().HasMaxLength(100).HasColumnName("LastName").HasColumnOrder(4);
            Property(u => u.Alias).IsRequired().HasMaxLength(100).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Alias", 3) { IsUnique = true } })).HasColumnName("Alias").HasColumnOrder(5);
            Property(u => u.EmailAddress).IsRequired().HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_EmailAdress", 4) { IsUnique = true } })).HasColumnName("EmailAddress").HasColumnOrder(6);
            Property(u => u.LoginEnabled).IsRequired().HasColumnName("LoginEnabled").HasColumnOrder(7);

            #endregion
        }
    }
}