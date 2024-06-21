﻿using System;
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
    public class PanelsController : Controller
    {
        private readonly GlassOptContext _context;

        public PanelsController(GlassOptContext context)
        {
            _context = context;
        }

        // GET: Panels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Panel.ToListAsync());
        }

        // GET: Panels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panel = await _context.Panel
                .FirstOrDefaultAsync(m => m.Panel_Id == id);
            if (panel == null)
            {
                return NotFound();
            }

            return View(panel);
        }

        // GET: Panels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Panels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Panel_Id,Width,Height,Qty")] Panel panel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(panel);
        }

        // GET: Panels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panel = await _context.Panel.FindAsync(id);
            if (panel == null)
            {
                return NotFound();
            }
            return View(panel);
        }

        // POST: Panels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Panel_Id,Width,Height,Qty")] Panel panel)
        {
            if (id != panel.Panel_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanelExists(panel.Panel_Id))
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
            return View(panel);
        }

        // GET: Panels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panel = await _context.Panel
                .FirstOrDefaultAsync(m => m.Panel_Id == id);
            if (panel == null)
            {
                return NotFound();
            }

            return View(panel);
        }

        // POST: Panels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var panel = await _context.Panel.FindAsync(id);
            if (panel != null)
            {
                _context.Panel.Remove(panel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanelExists(int id)
        {
            return _context.Panel.Any(e => e.Panel_Id == id);
        }
    }
}
