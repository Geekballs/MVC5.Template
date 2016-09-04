using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    public class UserRole : BaseEntity
    {
        #region Properties

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }
        public Role Role { get; set; }

        #endregion
    }
}