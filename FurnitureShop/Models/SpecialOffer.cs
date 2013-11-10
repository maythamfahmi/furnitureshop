using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
    public class SpecialOffer
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int SpecialOfferId { get; set; }

        public string Name { get; set; }

        public int ProductId1 { get; set; }

        public int ProductId2 { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "please enter a positive price")]
        public decimal Price { get; set; }
    }
}