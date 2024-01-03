using System.Collections.Generic;

namespace DismasDemo.Models
{
    public class PriceList
    {
        public int PriceListID { get; set; }
        public string PriceListName { get; set; }

        public List<Price> Prices { get; set; }
    }
}
