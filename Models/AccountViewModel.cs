using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CS872_WebApp.Models
{
    [Table("Account")]
    public class AccountViewModel
    {
        [Key]
        public Guid accountID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string emailAddress  { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string status { get; set; }

        [Required]
        [Display(Name = "Date Opened")]
        public DateTime dateOpened { get; set; }

        [Display(Name = "Date Closed")]
        public DateTime dateClosed { get; set; }

        [Required]
        [Display(Name = "Last Modified")]
        public DateTime lastModified { get; set; }

    }
}