using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

namespace CS872_WebApp.Models
{
    public class LoginViewModel : UserViewModel
    {

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }


        [Display(Name = "Status")]
        public string status { get { return "Active" ; } set { value = "Active" ; } }

     
        [HiddenInput(DisplayValue=false)]
        public new string userType { get { return "Standard"; } set { value = "Standard"; } }


    }
}