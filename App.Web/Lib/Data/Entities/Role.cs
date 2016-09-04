using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        #region Properties

        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}