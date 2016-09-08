using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using App.Web.Lib.Models;

namespace App.Web.Lib.ViewModels
{
    public class UserVm
    {
        public class Index
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int UserRoleCount { get; set; }
            public bool UserEnabled { get; set; }
            public bool UserLocked { get; set; }
        }

        public class Detail
        {
            [DisplayName("ID")]
            public Guid UserId { get; set; }

            [DisplayName("User Name")]
            public string UserName { get; set; }

            [DisplayName("First Name")]
            public string FirstName { get; set; }

            [DisplayName("Last Name")]
            public string LastName { get; set; }

            [DisplayName("Enabled")]
            public bool UserEnabled { get; set; }

            [DisplayName("Locked")]
            public bool UserLocked { get; set; }

            [DisplayName("Roles")]
            public List<UserRolesDetail> UserRolesDetail { get; set; }
        }

        public class UserRolesDetail
        {
            public Guid RoleId { get; set; }
            public string RoleName { get; set; }
        }

        public class Create
        {
            [DisplayName("User Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string UserName { get; set; }

            [DisplayName("First Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string FirstName { get; set; }

            [DisplayName("Last Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string LastName { get; set; }

            [DisplayName("Enabled")]
            public bool UserEnabled { get; set; }

            [DisplayName("Locked")]
            public bool UserLocked { get; set; }

            public List<CheckBoxListItem> Roles { get; set; }
        }

        public class Edit
        {
            public Guid UserId { get; set; }

            [DisplayName("User Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string UserName { get; set; }

            [DisplayName("First Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string FirstName { get; set; }

            [DisplayName("Last Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string LastName { get; set; }

            [DisplayName("Enabled")]
            public bool UserEnabled { get; set; }

            [DisplayName("Locked")]
            public bool UserLocked { get; set; }

            public List<CheckBoxListItem> Roles { get; set; }
        }

        public class Delete
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool UserEnabled { get; set; }
            public bool UserLocked { get; set; }
        }
    }
}