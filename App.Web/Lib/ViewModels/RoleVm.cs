using System;
using System.Collections.Generic;

namespace App.Web.Lib.ViewModels
{
    public class RoleVm
    {
        public class Index
        {
            public Guid RoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleDescription { get; set; }
            public int RoleUserCount { get; set; }
            public bool RoleEnabled { get; set; }
            public bool RoleLocked { get; set; }

        }

        public class Detail
        {
            public Guid RoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleDescription { get; set; }
            public bool RoleEnabled { get; set; }
            public bool RoleLocked { get; set; }
            public List<RoleUsersDetail> RoleUsersDetail { get; set; }
        }

        public class RoleUsersDetail
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }

        }

        public class Create
        {
            public string RoleName { get; set; }
            public string RoleDescription { get; set; }
        }

        public class Edit
        {
            public Guid RoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleDescription { get; set; }
            public bool RoleEnabled { get; set; }
            public bool RoleLocked { get; set; }
        }

        public class Delete
        {
            public Guid RoleId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool RoleEnabled { get; set; }
            public bool RoleLocked { get; set; }
        }
    }
}