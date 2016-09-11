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
            public string UserFirstName { get; set; }
            public string UserLastName { get; set; }
            public string UserAlias { get; set; }
            public string EmailAddress { get; set; }
            public bool LoginEnabled { get; set; }
            public int UserRoleCount { get; set; }
        }

        public class Detail
        {
            [DisplayName("ID")]
            public Guid UserId { get; set; }

            [DisplayName("User Name")]
            public string UserName { get; set; }

            [DisplayName("First Name")]
            public string UserFirstName { get; set; }

            [DisplayName("Last Name")]
            public string UserLastName { get; set; }

            [DisplayName("Alias")]
            public string UserAlias { get; set; }

            [DisplayName("Email Address")]
            public string UserEmailAddress { get; set; }

            [DisplayName("Login Enabled")]
            public bool UserLoginEnabled { get; set; }

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
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string UserName { get; set; }

            [DisplayName("First Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string UserFirstName { get; set; }

            [DisplayName("Last Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string UserLastName { get; set; }

            [DisplayName("Alias")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string UserAlias { get; set; }

            [DisplayName("Email Address")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [EmailAddress, DataType(DataType.EmailAddress, ErrorMessage = "Valid Email Address Required!")]
            public string UserEmailAddress { get; set; }

            [DisplayName("Login Enabled")]
            public bool UserLoginEnabled { get; set; }

            public List<CheckBoxListItem> Roles { get; set; }
        }

        public class Edit
        {
            public Guid UserId { get; set; }

            [DisplayName("User Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string UserName { get; set; }

            [DisplayName("First Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string UserFirstName { get; set; }

            [DisplayName("Last Name")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string UserLastName { get; set; }

            [DisplayName("Alias")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [RegularExpression("[A-Za-z0-9]*", ErrorMessage = "Alphanumeric Characters Only Please!")]
            public string UserAlias { get; set; }

            [DisplayName("Email Address")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "Maximum {1} Characters Exceeded!")]
            [EmailAddress, DataType(DataType.EmailAddress, ErrorMessage = "Valid Email Address Required!")]
            public string UserEmailAddress { get; set; }

            [DisplayName("Login Enabled")]
            public bool UserLoginEnabled { get; set; }

            public List<CheckBoxListItem> Roles { get; set; }
        }

        public class Delete
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string UserFirstName { get; set; }
            public string UserLastName { get; set; }
            public string UserAlias { get; set; }
            public string EmailAddress { get; set; }
            public bool UserLoginEnabled { get; set; }
        }
    }
}