﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using App.Web.Lib.Data.Entities;

namespace App.Web.Lib.Data.Maps
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            #region Table

            ToTable("Role", schemaName: "Membership");

            #endregion

            #region Keys

            HasKey(k => new { k.RoleId });

            #endregion

            #region Relationships

            // Nothing to see here!

            #endregion

            #region Properties

            Property(p => p.RoleId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_RoleId", 1) { IsUnique = true } })).HasColumnName("RoleId").HasColumnOrder(1);
            Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("IX_Name", 2) { IsUnique = true } })).HasColumnName("Name").HasColumnOrder(2);
            Property(p => p.Description).HasMaxLength(450).IsRequired().HasColumnName("Description").HasColumnOrder(3);

            #endregion
        }
    }
}