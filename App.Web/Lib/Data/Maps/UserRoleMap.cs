using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            #region Table

            ToTable("UserRole", schemaName: "Membership");

            #endregion

            #region Keys

            HasKey(k => new { k.UserId, k.RoleId });

            #endregion

            #region Relationships

            HasRequired(r => r.User);
            HasRequired(r => r.Role);

            #endregion

            #region Properties

            Property(p => p.UserId).IsRequired().HasColumnName("UserId").HasColumnOrder(1);
            Property(p => p.RoleId).IsRequired().HasColumnName("RoleId").HasColumnOrder(2);

            #endregion
        }
    }
}