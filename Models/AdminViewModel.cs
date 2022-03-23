using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web;

namespace CS872_WebApp.Models
{
    public class AdminViewModel : UserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }


        [Display(Name = "Status")]
        public string status { get { return "Active"; } set { value = "Active"; } }


    }
}