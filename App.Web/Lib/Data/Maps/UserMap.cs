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

            HasKey(x => new { x.UserId });

            #endregion

            #region Relationships

            // Nothing to see here!

            #endregion

            #region Properties

            Property(x => x.UserId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_UserId", 1) { IsUnique = true } })).HasColumnName("UserId").HasColumnOrder(1);
            Property(x => x.Name).IsRequired().HasMaxLength(450).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_UserName", 2) { IsUnique = true } })).HasColumnName("Name").HasColumnOrder(2);

            #endregion
        }
    }
}