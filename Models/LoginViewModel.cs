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
        public string userPassword { get; set; }


        [Display(Name = "Status")]
        public string userStatus { get; set; }


    }
}