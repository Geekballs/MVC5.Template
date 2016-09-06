using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
            [DisplayName("ID")]
            public Guid RoleId { get; set; }

            [DisplayName("Name")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            public string RoleDescription { get; set; }

            [DisplayName("Enabled")]
            public bool RoleEnabled { get; set; }

            [DisplayName("Locked")]
            public bool RoleLocked { get; set; }

            [DisplayName("Role Users")]
            public List<RoleUsersDetail> RoleUsersDetail { get; set; }
        }

        public class RoleUsersDetail
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }

        }

        public class Create
        {
            [DisplayName("Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string RoleDescription { get; set; }

            [DisplayName("Enabled")]
            public bool RoleEnabled { get; set; }

            [DisplayName("Locked")]
            public bool RoleLocked { get; set; }
        }

        public class Edit
        {
            public Guid RoleId { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            public string RoleDescription { get; set; }

            [DisplayName("Enabled")]
            public bool RoleEnabled { get; set; }

            [DisplayName("Locked")]
            public bool RoleLocked { get; set; }
        }

        public class Delete
        {
            public Guid RoleId { get; set; }

            [DisplayName("Name")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            public string RoleDescription { get; set; }

            [DisplayName("Enabled")]
            public bool RoleEnabled { get; set; }

            [DisplayName("Locked")]
            public bool RoleLocked { get; set; }
        }
    }
}