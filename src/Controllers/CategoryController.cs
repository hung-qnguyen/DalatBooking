using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApp.Models;
using BookingApp.Data;
using Microsoft.AspNetCore.Authorization;
using BookingApp.Utilities;

namespace BookingApp.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly HotelContext _context;

        public CategoryController(HotelContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return _context.HotelCategory != null
                ? View(await _context.HotelCategory.ToListAsync())
                : Problem("Entity set 'HotelContext.HotelCategory'  is null.");
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HotelCategory == null)
            {
                return NotFound();
            }

            var hotelCategory = await _context.HotelCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (hotelCategory == null)
            {
                return NotFound();
            }

            return View(hotelCategory);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] HotelCategory hotelCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotelCategory);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HotelCategory == null)
            {
                return NotFound();
            }

            var hotelCategory = await _context.HotelCategory.FindAsync(id);
            if (hotelCategory == null)
            {
                return NotFound();
            }
            return View(hotelCategory);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] HotelCategory hotelCategory)
        {
            if (id != hotelCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelCategoryExists(hotelCategory.Id))
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
            return View(hotelCategory);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HotelCategory == null)
            {
                return NotFound();
            }

            var hotelCategory = await _context.HotelCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (hotelCategory == null)
            {
                return NotFound();
            }

            return View(hotelCategory);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HotelCategory == null)
            {
                return Problem("Entity set 'HotelContext.HotelCategory'  is null.");
            }
            var hotelCategory = await _context.HotelCategory.FindAsync(id);
            if (hotelCategory != null)
            {
                _context.HotelCategory.Remove(hotelCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelCategoryExists(int id)
        {
            return (_context.HotelCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
