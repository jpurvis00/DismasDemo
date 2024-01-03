using DismasDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DismasDemo.Controllers
{
    public class PriceDashboardController : Controller
    {
        private readonly InventoryContext _context;

        public PriceDashboardController(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> PriceDashboard()
        {
            var priceList = await _context.PriceLists
                .Include(p => p.Prices)
                .ThenInclude(p => p.Part)
                .AsNoTracking()
                .ToListAsync();

            if (priceList == null)
            {
                return NotFound();
            }

            return View(priceList);
        }
    }
}
