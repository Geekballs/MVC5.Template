using System;
using System.Collections.Generic;

namespace App.Web.Lib.Data.Entities
{
    public class Department : BaseEntity
    {
        public Department()
        {
            Teams = new HashSet<Team>();
        }

        #region Properties

        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<Team> Teams { get; set; }

        #endregion
    }
}