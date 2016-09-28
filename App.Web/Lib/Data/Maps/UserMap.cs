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

            ToTable("User", schemaName: "Membership");

            #endregion

            #region Keys

            HasKey(k => new { k.UserId });

            #endregion

            #region Relationships

            HasMany(r => r.UserRoles).WithRequired(r => r.User);
            HasMany(r => r.UserTeams).WithRequired(r => r.User);

            #endregion

            #region Properties

            Property(p => p.UserId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_UserId", 1) { IsUnique = true } })).HasColumnName("UserId").HasColumnOrder(1);
            Property(p => p.UserName).IsRequired().HasMaxLength(100).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_UserName", 2) { IsUnique = true } })).HasColumnName("UserName").HasColumnOrder(2);
            Property(p => p.FirstName).IsRequired().HasMaxLength(100).HasColumnName("FirstName").HasColumnOrder(3);
            Property(p => p.LastName).IsRequired().HasMaxLength(100).HasColumnName("LastName").HasColumnOrder(4);
            Property(p => p.Alias).IsRequired().HasMaxLength(100).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Alias", 3) { IsUnique = true } })).HasColumnName("Alias").HasColumnOrder(5);
            Property(p => p.EmailAddress).IsRequired().HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_EmailAddress", 4) { IsUnique = true } })).HasColumnName("EmailAddress").HasColumnOrder(6);
            Property(p => p.LoginEnabled).IsRequired().HasColumnName("LoginEnabled").HasColumnOrder(7);

            #endregion
        }
    }
}