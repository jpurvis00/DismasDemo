using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DismasDemo.Data;
using DismasDemo.Models;

namespace DismasDemo.Controllers
{
    public class PriceListsController : Controller
    {
        private readonly InventoryContext _context;

        public PriceListsController(InventoryContext context)
        {
            _context = context;
        }

        // GET: PriceLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.PriceLists.ToListAsync());
        }

        // GET: PriceLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceList = await _context.PriceLists
                .Include(p => p.Prices)
                .ThenInclude(p => p.Part)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PriceListID == id);

            if (priceList == null)
            {
                return NotFound();
            }

            return View(priceList);
        }

        // GET: PriceLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriceLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PriceListName")] PriceList priceList)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context.Add(priceList);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(priceList);
        }

        // GET: PriceLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceList = await _context.PriceLists.FindAsync(id);
            if (priceList == null)
            {
                return NotFound();
            }
            return View(priceList);
        }

        // POST: PriceLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var priceListToUpdate = await _context.PriceLists.FirstOrDefaultAsync(s => s.PriceListID == id);
            
            if (await TryUpdateModelAsync<PriceList>(
                priceListToUpdate,
                "",
                s => s.PriceListName))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(priceListToUpdate);
        }

        // GET: PriceLists/EditPrice/5
        public async Task<IActionResult> EditPrice(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Price.FindAsync(id);
            
            
            if (price == null)
            {
                return NotFound();
            }
            
            return View(price);
        }

        // POST: PriceLists/EditPrice/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("EditPrice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPricePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Price.FirstOrDefaultAsync(p => p.PriceID == id);
            
            if (await TryUpdateModelAsync<Price>(
                price,
                "",
                p => p.ListPrice))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "PriceLists", new { id = price.PriceListID });
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(price);
        }

        // GET: PriceLists/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceList = await _context.PriceLists
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PriceListID == id);
            if (priceList == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(priceList);
        }

        // POST: PriceLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priceList = await _context.PriceLists.FindAsync(id);

            if (priceList == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.PriceLists.Remove(priceList);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)                                
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            
            return RedirectToAction(nameof(Index));
        }

        // GET: PriceLists/DeletePartFromPriceList/5
        public async Task<IActionResult> DeletePartFromPriceList(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Price
                            .Include(p => p.Part)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.PriceID == id);

            if (price == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(price);
        }

        // POST: PriceLists/DeletePartFromPriceList/5
        [HttpPost, ActionName("DeletePartFromPriceList")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePartFromPriceListConfirmed(int id)
        {
            var price = await _context.Price.FindAsync(id);

            if (price == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Price.Remove(price);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Details", "PriceLists", new { id = price.PriceListID });
        }

        private bool PriceListExists(int id)
        {
            return _context.PriceLists.Any(e => e.PriceListID == id);
        }
    }
}
