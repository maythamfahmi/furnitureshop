using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
    public class RatePlusComment
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int RateId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please specify a rate")]
        public int Rate { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a comment")]
        public string Comment { get; set; }

    }
}