namespace DismasDemo.Models
{
    public class Price
    {
        public int PriceID { get; set; }
        public int PartID { get; set; }
        public decimal ListPrice { get; set; }
        public int PriceListID { get; set; }

        public Part Part { get; set; } // Navigation property
    }
}
