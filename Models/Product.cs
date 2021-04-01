using System.Collections.Generic;

namespace AlifAdminMiniMarketV2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Basket> Basket { get; set; }
    }
}