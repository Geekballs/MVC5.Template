using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Lib.Data.Entities
{
    public class Team : BaseEntity
    {
        public Team()
        {
            UserTeams = new HashSet<UserTeam>();
        }

        #region Properties

        public Guid TeamId { get; set; }
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Department Department { get; set; }
        public virtual ICollection<UserTeam> UserTeams { get; set; }

        #endregion
    }
}