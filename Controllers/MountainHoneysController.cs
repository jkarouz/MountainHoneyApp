using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MountainHoneyApp.Models;

namespace MountainHoneyApp.Controllers
{
    public class MountainHoneysController : Controller
    {
        private readonly TestingContext _context;

        public MountainHoneysController(TestingContext context)
        {
            _context = context;
        }

        // GET: MountainHoneys
        public async Task<IActionResult> Index()
        {
              return _context.MountainHoneys != null ? 
                          View(await _context.MountainHoneys.ToListAsync()) :
                          Problem("Entity set 'TestingContext.MountainHoneys'  is null.");
        }

        // GET: MountainHoneys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MountainHoneys == null)
            {
                return NotFound();
            }

            var mountainHoney = await _context.MountainHoneys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mountainHoney == null)
            {
                return NotFound();
            }

            return View(mountainHoney);
        }

        // GET: MountainHoneys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MountainHoneys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,FullName,IdNumber,ContactNumber,RentAmount,Date")] MountainHoney mountainHoney)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mountainHoney);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mountainHoney);
        }

        // GET: MountainHoneys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MountainHoneys == null)
            {
                return NotFound();
            }

            var mountainHoney = await _context.MountainHoneys.FindAsync(id);
            if (mountainHoney == null)
            {
                return NotFound();
            }
            return View(mountainHoney);
        }

        // POST: MountainHoneys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,FullName,IdNumber,ContactNumber,RentAmount,Date")] MountainHoney mountainHoney)
        {
            if (id != mountainHoney.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mountainHoney);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MountainHoneyExists(mountainHoney.Id))
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
            return View(mountainHoney);
        }

        // GET: MountainHoneys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MountainHoneys == null)
            {
                return NotFound();
            }

            var mountainHoney = await _context.MountainHoneys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mountainHoney == null)
            {
                return NotFound();
            }

            return View(mountainHoney);
        }

        // POST: MountainHoneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MountainHoneys == null)
            {
                return Problem("Entity set 'TestingContext.MountainHoneys'  is null.");
            }
            var mountainHoney = await _context.MountainHoneys.FindAsync(id);
            if (mountainHoney != null)
            {
                _context.MountainHoneys.Remove(mountainHoney);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MountainHoneyExists(int id)
        {
          return (_context.MountainHoneys?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
