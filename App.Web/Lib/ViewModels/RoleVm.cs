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

        }

        public class Detail
        {
            [DisplayName("ID")]
            public Guid RoleId { get; set; }

            [DisplayName("Name")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            public string RoleDescription { get; set; }

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
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(450, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string RoleDescription { get; set; }
        }

        public class Edit
        {
            public Guid RoleId { get; set; }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(450, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string RoleDescription { get; set; }
        }

        public class Delete
        {
            public Guid RoleId { get; set; }

            [DisplayName("Name")]
            public string RoleName { get; set; }

            [DisplayName("Description")]
            public string RoleDescription { get; set; }
        }
    }
}