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
    public class MunicipalitiesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MunicipalitiesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Municipalities
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Municipalities.
                Include(m => m.Country).Include(m => m.Department).Include(m => m.Region);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Municipalities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipality = await _context.Municipalities
                .Include(m => m.Country)
                .Include(m => m.Department)
                .Include(m => m.Region)
                .FirstOrDefaultAsync(m => m.MunicipalityID == id);
            if (municipality == null)
            {
                return NotFound();
            }

            return View(municipality);
        }

        // GET: Municipalities/Create
        public IActionResult Create()
        {
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName");
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            ViewBag.RegionID = new SelectList(_context.Regions, "RegionID", "RegionName");
            return View();
        }

        // POST: Municipalities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MunicipalityID,CountryID,DepartmentID,RegionID,MunicipalityName,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(municipality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", municipality.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", municipality.DepartmentID);
            ViewBag.RegionID = new SelectList(_context.Regions, "RegionID", "RegionName", municipality.RegionID);
            return View(municipality);
        }

        // GET: Municipalities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Cargar el municipio con las propiedades de navegación
            var municipality = await _context.Municipalities
                .Include(m => m.Country)
                .Include(m => m.Department)
                .Include(m => m.Region)
                .FirstOrDefaultAsync(m => m.MunicipalityID == id);

            if (municipality == null)
            {
                return NotFound();
            }
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", municipality.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", municipality.DepartmentID);
            ViewBag.RegionID = new SelectList(_context.Regions, "RegionID", "RegionName", municipality.RegionID);
            return View(municipality);
        }

        // POST: Municipalities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MunicipalityID,CountryID,DepartmentID,RegionID,MunicipalityName,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] Municipality municipality)
        {
            if (id != municipality.MunicipalityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(municipality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MunicipalityExists(municipality.MunicipalityID))
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
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", municipality.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", municipality.DepartmentID);
            ViewBag.RegionID = new SelectList(_context.Regions, "RegionID", "RegionName", municipality.RegionID);
            return View(municipality);
        }

        // GET: Municipalities/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipality = await _context.Municipalities
                .Include(m => m.Country)
                .Include(m => m.Department)
                .Include(m => m.Region)
                .FirstOrDefaultAsync(m => m.MunicipalityID == id);
            if (municipality == null)
            {
                return NotFound();
            }

            return View(municipality);
        }

        // POST: Municipalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var municipality = await _context.Municipalities.FindAsync(id);

            if (municipality != null)
            {
                _context.Municipalities.Remove(municipality);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ---------- Funciones Adicionales ----------------- 
        private bool MunicipalityExists(int id)
        {
            return _context.Municipalities.Any(e => e.MunicipalityID == id);
        }

        // Métodos para cargar las listas dinámicas
        public async Task<JsonResult> GetDepartments(int countryId)
        {
            var departments = await _context.Departments
                .Where(d => d.CountryID == countryId)
                .Select(d => new { id = d.DepartmentID, name = d.DepartmentName })
                .ToListAsync();
            return Json(departments);
        }

        public async Task<JsonResult> GetRegions(int departmentId)
        {
            var regions = await _context.Regions
                .Where(r => r.DepartmentID == departmentId)
                .Select(r => new { id = r.RegionID, name = r.RegionName })
                .ToListAsync();
            return Json(regions);
        }
    }
}
