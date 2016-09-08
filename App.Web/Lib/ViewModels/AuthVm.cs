using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace App.Web.Lib.ViewModels
{
    public class AuthVm
    {
        public class SignIn
        {
            [Required, AllowHtml]
            public string Username { get; set; }

            [Required]
            [AllowHtml]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public class UserProperties
        {
            public string Username { get; set; }
            public bool IsAuthenticated { get; set; }
            public bool IsSuperAdminRole { get; set; }
            public bool IsAdminRole { get; set; }
        }
    }
}
