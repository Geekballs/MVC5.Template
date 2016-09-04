using System;
using System.Collections.Generic;
using App.Web.Lib.Models;

namespace App.Web.Lib.ViewModels
{
    public class UserVm
    {
        public class Index
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public int UserRoleCount { get; set; }
            public bool UserEnabled { get; set; }
            public bool UserLocked { get; set; }
        }

        public class Detail
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public bool UserEnabled { get; set; }
            public bool UserLocked { get; set; }
            public List<UserRolesDetail> UserRolesDetail { get; set; }
        }

        public class UserRolesDetail
        {
            public Guid RoleId { get; set; }
            public string RoleName { get; set; }
        }

        public class Create
        {
            public string UserName { get; set; }
            public List<CheckBoxListItem> Roles { get; set; }
        }

        public class Edit
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public bool UserEnabled { get; set; }
            public bool UserLocked { get; set; }
            public List<CheckBoxListItem> Roles { get; set; }
        }

        public class Delete
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public bool UserEnabled { get; set; }
            public bool UserLocked { get; set; }
        }
    }
}