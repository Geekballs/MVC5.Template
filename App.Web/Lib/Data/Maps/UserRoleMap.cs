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

            HasKey(ur => new { ur.UserId, ur.RoleId });

            #endregion

            #region Relationships

            HasRequired(u => u.User).WithMany(ur => ur.UserRoles).HasForeignKey(u => u.UserId);
            HasRequired(r => r.Role).WithMany(ur => ur.UserRoles).HasForeignKey(r => r.RoleId);

            #endregion

            #region Properties

            Property(ur => ur.UserId).IsRequired().HasColumnName("UserId").HasColumnOrder(1);
            Property(ur => ur.RoleId).IsRequired().HasColumnName("RoleId").HasColumnOrder(2);

            #endregion
        }
    }
}