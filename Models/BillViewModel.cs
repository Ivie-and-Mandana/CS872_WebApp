using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CS872_WebApp.Models
{
    [Table("Bill")]
    public class BillViewModel
    {
        [Key]
        public Guid billID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }

        [Required]
        public string userType { get; set; }

 
        [Required]
        [Display(Name = "Bill Date")]
        public DateTime billDateTIme { get; set; }

        [Required]
        [Display(Name = "Bill Amount")]
        public decimal amount { get; set; }

        [Required]
        [Display(Name = "Bill Status")]
        public string billStatus { get; set; }

        [Required]
        [Display(Name = "House Area")]
        public decimal houseArea { get; set; }

        [Required]
        [Display(Name = "Number of Rooms")]
        public int numberOfRooms { get; set; }


        [Required]
        [Display(Name = "Number of Children")]
        public int numberOfChildren { get; set; }


        [Required]
        [Display(Name = "Number of People")]
        public int numberOfPeople { get; set; }


        [Required]
        [Display(Name = "Includes Air Condition")]
        public bool isAirCondtion { get; set; }


        [Required]
        [Display(Name = "Includes Television")]
        public bool isTelevision { get; set; }


        [Required]
        [Display(Name = "Flat")]
        public bool isFlat { get; set; }


        [Required]
        [Display(Name = "Urban/Rural")]
        public bool isUrban { get; set; }
    }
}