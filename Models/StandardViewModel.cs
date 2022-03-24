using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CS872_WebApp.Models
{
    public class StandardViewModel : UserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string userPassword { get; set; }


        [Display(Name = "Status")]
        public string userStatus { get; set; }

    }
}