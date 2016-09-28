using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    public class TeamMap : EntityTypeConfiguration<Team>
    {
        public TeamMap()
        {
            #region Table

            ToTable("Team", schemaName: "Membership");

            #endregion

            #region Keys

            HasKey(k => new { k.TeamId });

            #endregion

            #region Relationships

            HasRequired(r => r.Department);

            #endregion

            #region Properties

            Property(p => p.TeamId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_TeamId", 1) { IsUnique = true } })).HasColumnName("TeamId").HasColumnOrder(1);
            Property(p => p.DepartmentId).IsRequired().HasColumnName("DepartmentId").HasColumnOrder(2);
            Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnName("Name").HasColumnOrder(3);
            Property(p => p.Description).HasMaxLength(450).IsRequired().HasColumnName("Description").HasColumnOrder(4);

            #endregion
        }
    }
}