using System.Collections.Generic;

namespace FurnitureShop.Models
{
    public class ProductListView
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}