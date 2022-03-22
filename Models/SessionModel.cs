using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CS872_WebApp.Models
{
    [Table("Session")]
    public class SessionModel
    {
        [Key]
        public int sessionID { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public int emailAddress { get; set; }


        [Required]
        public string status { get; set; }

        [Required]
        public DateTime loginTime  { get; set; }


        public DateTime logoutTime { get; set; }

        

    }
}