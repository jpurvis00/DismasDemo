using System.Collections.Generic;
using System.Linq;
using DismasDemo.Models;
using static DismasDemo.Models.PriceList;

namespace DismasDemo.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InventoryContext context)
        {
            context.Database.EnsureCreated();

            //Look for any parts
            if (context.Part.Any())
            {
                return; //Db has already been created.
            }

            var parts = new Part[]
            {
                new Part{Sku="A100", Description="ACE Retro Gen 1 ARFX Stock", Quantity=5 },
                new Part{Sku="A101", Description="ACE ARFX Stock w/ Blk Foam & .5\" RP", Quantity=15 },
                new Part{Sku="AR100", Description="Bolt Carrier Group W/ O Ring 5.56", Quantity=25 },
                new Part{Sku="AR101", Description="Bolt Carrier, Stripped 5.56", Quantity=35 },
                new Part{Sku="AR102", Description="Bolt Carrier & Key", Quantity=45 },
                new Part{Sku="AR103", Description="Bolt Cam Pin", Quantity=55 }
            };

            foreach (var p in parts)
            {
                context.Part.Add(p);
            }

            context.SaveChanges();

            var priceLists = new PriceList[]
            {
                new PriceList{PriceListName="Retail",
                    Prices = new List<Price>()
                    {
                        new Price { PartID = 1, ListPrice = 5.00M },
                        new Price { PartID = 2, ListPrice = 6.00M },
                        new Price { PartID = 3, ListPrice = 7.00M },
                        new Price { PartID = 4, ListPrice = 8.00M },
                        new Price { PartID = 5, ListPrice = 9.00M },
                        new Price { PartID = 6, ListPrice = 10.00M },
                    } 
                },
                new PriceList{PriceListName="MAP",
                    Prices = new List<Price>()
                    {
                        new Price { PartID = 1, ListPrice = 4.00M },
                        new Price { PartID = 2, ListPrice = 5.00M },
                        new Price { PartID = 3, ListPrice = 6.00M },
                        new Price { PartID = 4, ListPrice = 7.00M },
                        new Price { PartID = 5, ListPrice = 8.00M },
                        new Price { PartID = 6, ListPrice = 9.00M },
                    }
                },
                new PriceList{PriceListName="Dealer",
                    Prices = new List<Price>()
                    {
                        new Price { PartID = 1, ListPrice = 3.00M },
                        new Price { PartID = 2, ListPrice = 4.00M },
                        new Price { PartID = 3, ListPrice = 5.00M },
                        new Price { PartID = 4, ListPrice = 6.00M },
                        new Price { PartID = 5, ListPrice = 7.00M },
                        new Price { PartID = 6, ListPrice = 8.00M },
                    }
                }
            };

            foreach (var l in priceLists)
            {
                context.PriceLists.Add(l);
            }
            
            context.SaveChanges();
        }
    }
}


