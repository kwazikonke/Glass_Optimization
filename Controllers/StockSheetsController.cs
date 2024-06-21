using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlassOpt.Data;
using GlassOpt.Models;

namespace GlassOpt.Controllers
{
    public class StockSheetsController : Controller
    {
        private readonly GlassOptContext _context;

        public StockSheetsController(GlassOptContext context)
        {
            _context = context;
        }

        // GET: StockSheets
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockSheet.ToListAsync());
        }

        // GET: StockSheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockSheet = await _context.StockSheet
                .FirstOrDefaultAsync(m => m.StockSheet_Id == id);
            if (stockSheet == null)
            {
                return NotFound();
            }

            return View(stockSheet);
        }

        // GET: StockSheets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockSheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockSheet_Id,Width,Height,Qty,Cost")] StockSheet stockSheet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockSheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockSheet);
        }

        // GET: StockSheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockSheet = await _context.StockSheet.FindAsync(id);
            if (stockSheet == null)
            {
                return NotFound();
            }
            return View(stockSheet);
        }

        // POST: StockSheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockSheet_Id,Width,Height,Qty,Cost")] StockSheet stockSheet)
        {
            if (id != stockSheet.StockSheet_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockSheetExists(stockSheet.StockSheet_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(stockSheet);
        }

        // GET: StockSheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockSheet = await _context.StockSheet
                .FirstOrDefaultAsync(m => m.StockSheet_Id == id);
            if (stockSheet == null)
            {
                return NotFound();
            }

            return View(stockSheet);
        }

        // POST: StockSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockSheet = await _context.StockSheet.FindAsync(id);
            if (stockSheet != null)
            {
                _context.StockSheet.Remove(stockSheet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockSheetExists(int id)
        {
            return _context.StockSheet.Any(e => e.StockSheet_Id == id);
        }
    }
}
