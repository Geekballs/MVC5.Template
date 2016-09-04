using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        #region Properties

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}