using System.Collections.Generic;
﻿using DismasDemo.Data;
using DismasDemo.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DismasDemo.Repository
{
    public class InventoryRepository
    {
        string connectionString;
        private readonly InventoryContext _dbContext;


        public InventoryRepository(string connectionString, InventoryContext dbContext)
        {
            this.connectionString = connectionString;
            this._dbContext = dbContext;
        }

        public List<PriceList> GetProducts()
        {

            var priceList = _dbContext.PriceLists
               .Include(p => p.Prices)
               .ThenInclude(p => p.Part)
               .AsNoTracking()
               .ToList();

            foreach (var inv in priceList)
            {
                _dbContext.Entry(inv).Reload();
            }

            return priceList;
        }
    }
}
