using System;

namespace App.Web.Lib.Models
{
    public class CheckBoxListItem
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public bool IsChecked { get; set; }
    }
}