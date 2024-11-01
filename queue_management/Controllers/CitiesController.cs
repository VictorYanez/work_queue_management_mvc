using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using queue_management.Data;
using queue_management.Models;

namespace queue_management.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CitiesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Cities
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Cities
                .Include(c => c.Municipality)
                .ThenInclude(m => m.Region)
                .ThenInclude(r => r.Department)
                .ThenInclude(d => d.Country);
            return View(await applicationDBContext.ToListAsync());

        }

        // GET: Cities/Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xcity = await _context.Cities
                .Include(c => c.Municipality)
                .FirstOrDefaultAsync(m => m.CityID == id);

            var city = await _context.Cities
                .Include(c => c.Municipality)
                .ThenInclude(m => m.Region)
                .ThenInclude(r => r.Department)
                .ThenInclude(d => d.Country)
                .FirstOrDefaultAsync(m => m.CityID == id);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        [HttpGet]
        public IActionResult Create()
        {
            PopulateDropDown(); // Inicializar listas sin selección previa
            return View();
        }

        public IActionResult CreateCheck()
        {
            ViewBag.MunicipalityID = new SelectList(_context.Municipalities, "MunicipalityID", "MunicipalityName");
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityID,CityName,CountryID,DepartmentID,RegionID,MunicipalityID,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] City city)
        {
            // Remover propiedades de navegación del ModelState para evitar validación innecesaria
            ModelState.Remove("Region");
            ModelState.Remove("Country");
            ModelState.Remove("Department");
            ModelState.Remove("Municipality");

            // Validación de ForeignKey vacias 
            if (city.CountryID == 0 || city.DepartmentID == 0 || city.RegionID == 0 || city.MunicipalityID == 0)
            {
                ModelState.AddModelError("", "Seleccione todas las opciones de ubicación.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Registrar errores de ModelState
            foreach (var entry in ModelState)
            {
                if (entry.Value.Errors.Count > 0)
                {
                    Console.WriteLine($"Property: {entry.Key} - Errors: {string.Join(", ", entry.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }

            // Repoblar listas de selección
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName");
            ViewBag.DepartmentID = new SelectList(Enumerable.Empty<SelectListItem>(), "DepartmentID", "DepartmentName");
            ViewBag.RegionID = new SelectList(Enumerable.Empty<SelectListItem>(), "RegionID", "RegionName");
            ViewBag.MunicipalityID = new SelectList(Enumerable.Empty<SelectListItem>(), "MunicipalityID", "MunicipalityName");

            return View(city);
        }


        // GET: Cities/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.Municipality)
                .ThenInclude(m => m.Region)
                .ThenInclude(r => r.Department)
                .ThenInclude(d => d.Country)
                .FirstOrDefaultAsync(m => m.CityID == id);

            if (city == null)
            {
                return NotFound();
            }
            // Llenado de las listas
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", city.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments.Where(d => d.CountryID == city.CountryID), "DepartmentID", "DepartmentName", city.DepartmentID);
            ViewBag.RegionID = new SelectList(_context.Regions.Where(r => r.DepartmentID == city.DepartmentID), "RegionID", "RegionName", city.RegionID);
            ViewBag.MunicipalityID = new SelectList(_context.Municipalities.Where(m => m.RegionID == city.RegionID), "MunicipalityID", "MunicipalityName", city.MunicipalityID);
            return View(city);
        }

        // POST: Cities/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityID,CityName,CountryID,DepartmentID,RegionID,MunicipalityID,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] City city)
        {
            if (id != city.CityID)
            {
                return NotFound();
            }

            // Remover propiedades de navegación del ModelState para evitar validación innecesaria
            ModelState.Remove("Region");
            ModelState.Remove("Country");
            ModelState.Remove("Department");
            ModelState.Remove("Municipality");

            if (ModelState.IsValid)
            {
                try
                {
                    var countryExists = _context.Countries.Any(m => m.CountryID == city.CountryID);
                    var departmentExists = _context.Departments.Any(m => m.DepartmentID == city.MunicipalityID);
                    var regionExists = _context.Regions.Any(m => m.RegionID == city.RegionID);
                    var municipalityExists = _context.Municipalities.Any(m => m.MunicipalityID == city.MunicipalityID);
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.CityID))
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

            // Llenado de las listas
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", city.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments.Where(d => d.CountryID == city.CountryID), "DepartmentID", "DepartmentName", city.DepartmentID);
            ViewBag.RegionID = new SelectList(_context.Regions.Where(r => r.DepartmentID == city.DepartmentID), "RegionID", "RegionName", city.RegionID);
            ViewBag.MunicipalityID = new SelectList(_context.Municipalities.Where(m => m.RegionID == city.RegionID), "MunicipalityID", "MunicipalityName", city.MunicipalityID);
            //ViewBag.MunicipalityID = new SelectList(_context.Municipalities, "MunicipalityID", "MunicipalityName", city.MunicipalityID);
            return View(city);
        }

        // GET: Cities/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.Municipality)
                .ThenInclude(m => m.Region)
                .ThenInclude(r => r.Department)
                .ThenInclude(d => d.Country)
                .FirstOrDefaultAsync(m => m.CityID == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var xcity = await _context.Cities.FindAsync(id);
            var city = await _context.Cities
                .Include(c => c.Municipality)
                .ThenInclude(m => m.Region)
                .ThenInclude(r => r.Department)
                .ThenInclude(d => d.Country)
                .FirstOrDefaultAsync(m => m.CityID == id);
            if (city != null)
            {
                _context.Cities.Remove(city);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ---------- Funciones Adicionales ----------------- 
        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.CityID == id);
        }

        // Método para inicializar las listas desplegables
        private void PopulateDropDown(int? countryId = null, int? departmentId = null, int? regionId = null, int? municipalityId = null)
        {
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", countryId);
            ViewBag.DepartmentID = new SelectList(_context.Departments.Where(d => d.CountryID == countryId), "DepartmentID", "DepartmentName", departmentId);
            ViewBag.RegionID = new SelectList(_context.Regions.Where(r => r.DepartmentID == departmentId), "RegionID", "RegionName", regionId);
            ViewBag.MunicipalityID = new SelectList(_context.Municipalities.Where(m => m.RegionID == regionId), "MunicipalityID", "MunicipalityName", municipalityId);
        }

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

        public async Task<JsonResult> GetMunicipalities(int regionId)
        {
            var municipalities = await _context.Municipalities
                .Where(m => m.RegionID == regionId)
                .Select(m => new { id = m.MunicipalityID, name = m.MunicipalityName })
                .ToListAsync();
            return Json(municipalities);
        }
    }
}
