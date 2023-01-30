using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Danger.Data;
using Danger.Models;
using Microsoft.AspNetCore.Authorization;

namespace Danger.Controllers
{
    public class RandomizesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandomizesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Randomizes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Randomize.ToListAsync());
        }
        
        //GET: SearchForm
        public async Task<IActionResult> SearchForm()
        {
              return View();
        }
        
        public IActionResult SearchResult(String Searching)
        {
            return View("",await _context.Where(Searching)Randomize.ToListAsync());
        }

        
        
        // GET: Randomizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randomize == null)
            {
                return NotFound();
            }

            var randomize = await _context.Randomize
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randomize == null)
            {
                return NotFound();
            }

            return View(randomize);
        }

        // GET: Randomizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Randomizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comments,UserTag")] Randomize randomize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randomize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(randomize);
        }

        [Authorize]
        // GET: Randomizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Randomize == null)
            {
                return NotFound();
            }

            var randomize = await _context.Randomize.FindAsync(id);
            if (randomize == null)
            {
                return NotFound();
            }
            return View(randomize);
        }

        // POST: Randomizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comments,UserTag")] Randomize randomize)
        {
            if (id != randomize.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randomize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandomizeExists(randomize.Id))
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
            return View(randomize);
        }

        [Authorize]
        // GET: Randomizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randomize == null)
            {
                return NotFound();
            }

            var randomize = await _context.Randomize
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randomize == null)
            {
                return NotFound();
            }

            return View(randomize);
        }

        [Authorize]
        // POST: Randomizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randomize == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Randomize'  is null.");
            }
            var randomize = await _context.Randomize.FindAsync(id);
            if (randomize != null)
            {
                _context.Randomize.Remove(randomize);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandomizeExists(int id)
        {
          return _context.Randomize.Any(e => e.Id == id);
        }
    }
}
