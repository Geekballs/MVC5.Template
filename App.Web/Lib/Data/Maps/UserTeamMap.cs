using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    public class UserTeamMap : EntityTypeConfiguration<UserTeam>
    {
        public UserTeamMap()
        {
            #region Table

            ToTable("UserTeam", schemaName: "Membership");

            #endregion

            #region Keys

            HasKey(k => new { k.UserId, k.TeamId });

            #endregion

            #region Relationships

            HasRequired(r => r.User);
            HasRequired(r => r.Team);

            #endregion

            #region Properties

            Property(p => p.UserId).IsRequired().HasColumnName("UserId").HasColumnOrder(1);
            Property(p => p.TeamId).IsRequired().HasColumnName("TeamId").HasColumnOrder(2);

            #endregion
        }
    }
}