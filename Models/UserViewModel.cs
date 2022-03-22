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
    public class UserViewModel
    {
        [Key]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }


        [Display(Name = "Full Name")]
        public string fullName { get { return this.firstName + " " + this.lastName; } set { value = this.firstName + " " + this.lastName; } }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string province { get; set; }


        [Required]
        [Display(Name = "Postal Code")]
        public string postalCode { get; set; }


        [Required]
        [Display(Name = "User Type")]
        public string userType { get; set; }  
    }
}