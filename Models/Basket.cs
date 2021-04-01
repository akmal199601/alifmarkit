namespace AlifAdminMiniMarketV2.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public int Numbers { get; set; }
        public int DeliverTime { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
    }
}