using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            #region Table

            ToTable("Department", schemaName: "Membership");

            #endregion

            #region Keys

            HasKey(k => new { k.DepartmentId });

            #endregion

            #region Relationships

            HasMany(x => x.Teams).WithRequired(x => x.Department).WillCascadeOnDelete(false);

            #endregion

            #region Properties

            Property(p => p.DepartmentId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_DepartmentId", 1) { IsUnique = true } })).HasColumnName("DepartmentId").HasColumnOrder(1);
            Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Name", 2) { IsUnique = true } })).HasColumnName("Name").HasColumnOrder(2);
            Property(p => p.Description).HasMaxLength(450).IsRequired().HasColumnName("Description").HasColumnOrder(3);

            #endregion
        }
    }
}