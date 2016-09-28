using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    public class UserTeam : BaseEntity
    {
        #region Properties

        public Guid UserId { get; set; }
        public Guid TeamId { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }
        public Team Team { get; set; }

        #endregion
    }
}