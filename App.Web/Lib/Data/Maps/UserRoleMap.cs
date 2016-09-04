using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            #region Table

            ToTable("UserRole", schemaName: "Security");

            #endregion

            #region Keys

            HasKey(x => new { x.UserId, x.RoleId });

            #endregion

            #region Relationships

            HasRequired(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            HasRequired(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);

            #endregion

            #region Properties

            Property(x => x.UserId).IsRequired().HasColumnName("UserId").HasColumnOrder(1);
            Property(x => x.RoleId).IsRequired().HasColumnName("RoleId").HasColumnOrder(2);

            #endregion
        }
    }
}