using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Models
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter a your full name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter the first address line")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }
        public string Postal { get; set; }
        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }
    }
}