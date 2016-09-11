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
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public string EmailAddress { get; set; }
        public bool LoginEnabled { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}