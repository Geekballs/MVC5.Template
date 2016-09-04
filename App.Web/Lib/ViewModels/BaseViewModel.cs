using System;

namespace App.Web.Lib.ViewModels
{
    public abstract class BaseViewModel
    {
        public bool Enabled { get; set; }
        public bool Locked { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
