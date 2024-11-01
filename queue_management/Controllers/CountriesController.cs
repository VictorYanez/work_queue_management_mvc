using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using queue_management.Data;
using queue_management.Models;

namespace queue_management.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CountriesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Countries
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var countries = await _context.Countries.ToListAsync();

                if (countries == null || !countries.Any())
                {
                    ViewBag.Message = "No locations available.";
                }

                return View(countries);
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception and return a user-friendly error message
                ViewBag.ErrorMessage = "There was a problem with the database. Please contact the administrator.";
                // Optionally, you can return a custom error view:
                // return View("DatabaseError");
            }
            catch (Exception ex)
            {
                // Log the exception
                ViewBag.ErrorMessage = "An unexpected error occurred. Please contact the administrator.";
            }

            // In case of error, return an empty view or a custom error view
            return View(new List<Country>());
        }

        // GET: Countries/Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryID,CountryName")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryID,CountryName,RowVersion")] Country country)
        {
            if (id != country.CountryID)
            {
                return NotFound();
            }

            if (!CountryExists(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        // GET: Countries/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Definición de Funciones Misceláneas 
        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryID == id);
        }

        // Manejo de vistas de Excepciones 
    }
}
