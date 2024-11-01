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
    public class RegionsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public RegionsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Regions
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Regions.Include(r => r.Country).Include(r => r.Department);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Regions
                .Include(r => r.Country)
                .Include(r => r.Department)
                .FirstOrDefaultAsync(m => m.RegionID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Regions/Create
        public IActionResult Create()
        {
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName");
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            return View();
        }

        // POST: Regions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionID,RegionName,CountryID,DepartmentID,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] Region region)
        {
            if (ModelState.IsValid)
            {
                _context.Add(region);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", region.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", region.DepartmentID);
            return View(region);
        }

        // GET: Regions/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Regions
                .Include(r => r.Country)
                .Include(r => r.Department)
                .FirstOrDefaultAsync(m => m.RegionID == id);
            if (region == null)
            {
                return NotFound();
            }
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", region.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", region.DepartmentID);
            return View(region);
        }

        // POST: Regions/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegionID,RegionName,CountryID,DepartmentID,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] Region region)
        {
            if (id != region.RegionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(region);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionExists(region.RegionID))
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
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", region.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", region.DepartmentID);
            return View(region);
        }

        // GET: Regions/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Regions
                .Include(r => r.Country)
                .Include(r => r.Department)
                .FirstOrDefaultAsync(m => m.RegionID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Regions/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var region = await _context.Regions.FindAsync(id);
            if (region != null)
            {
                _context.Regions.Remove(region);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Funciones Adicionales 
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentID == id);
        }

        private bool RegionExists(int id)
        {
            return _context.Regions.Any(e => e.RegionID == id);
        }

        public async Task<JsonResult> GetDepartments(int countryId)
        {
            var departments = await _context.Departments
                .Where(d => d.CountryID == countryId)
                .Select(d => new { id = d.DepartmentID, name = d.DepartmentName })
                .ToListAsync();
            return Json(departments);
        }

    }
}
