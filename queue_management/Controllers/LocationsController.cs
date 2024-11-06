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
    public class LocationsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LocationsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {

            var applicationDBContext = _context.Locations
                .Include(c => c.City)
                .ThenInclude(c => c.Municipality)
                .ThenInclude(m => m.Region)
                .ThenInclude(r => r.Department)
                .ThenInclude(d => d.Country);
            return View(await applicationDBContext.ToListAsync());
//            var applicationDBContext = _context.Locations.Include(l => l.City).Include(l => l.Country).Include(l => l.Department).Include(l => l.Region);
//            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(y => y.City)
                .ThenInclude(x => x.Municipality)
                .ThenInclude(m => m.Region)
                .ThenInclude(d => d.Department)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(l => l.LocationID == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
           
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName");
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            ViewBag.RegionID = new SelectList(_context.Regions, "RegionID", "RegionName");
            ViewBag.CityID = new SelectList(_context.Cities, "CityID", "CityName");
            return View();
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationID,LocationName,PhoneNumber,CountryID,DepartmentID,RegionID,MunicipalityID,CityID,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] Location location)
        {
            // Remover propiedades de navegación del ModelState para evitar validación innecesaria
            ModelState.Remove("Region");
            ModelState.Remove("Country");
            ModelState.Remove("Department");
            ModelState.Remove("Municipality");
            ModelState.Remove("City");

            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Repoblar listas en caso de errores 
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", location.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments.Where(d => d.CountryID == location.CountryID), "DepartmentID", "DepartmentName", location.DepartmentID);
            ViewBag.RegionID = new SelectList(_context.Regions.Where(r => r.DepartmentID == location.DepartmentID), "RegionID", "RegionName", location.RegionID);
            ViewBag.MunicipalityID = new SelectList(_context.Municipalities.Where(m => m.RegionID == location.RegionID), "MunicipalityID", "MunicipalityName", location.MunicipalityID);
            ViewBag.CityID = new SelectList(_context.Cities.Where(c => c.MunicipalityID == location.MunicipalityID), "CityID", "CityName", location.CityID);

            return View(location);
        }

        // GET: Locations/Edit/
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                 .Include(l => l.City)
                 .ThenInclude(x => x.Municipality)
                 .ThenInclude(m => m.Region)
                 .ThenInclude(r => r.Department)
                 .ThenInclude(d => d.Country)
                 .FirstOrDefaultAsync(m => m.LocationID == id);

            if (location == null)
            {
                return NotFound();
            }
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", location.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments.Where(d => d.CountryID == location.CountryID), "DepartmentID", "DepartmentName", location.DepartmentID);
            ViewBag.RegionID = new SelectList(_context.Regions.Where(r => r.DepartmentID == location.DepartmentID), "RegionID", "RegionName", location.RegionID);
            ViewBag.MunicipalityID = new SelectList(_context.Municipalities.Where(m => m.RegionID == location.RegionID), "MunicipalityID", "MunicipalityName", location.MunicipalityID);
            ViewBag.CityID = new SelectList(_context.Cities, "CityID", "CityName", location.CityID);

            return View(location);
        }

        // POST: Locations/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationID,LocationName,PhoneNumber,CountryID,DepartmentID,RegionID,MunicipalityID,CityID,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] Location location)
        {
            if (id != location.LocationID)
            {
                return NotFound();
            }

            // Remover propiedades de navegación del ModelState para evitar validación innecesaria
            ModelState.Remove("Country");
            ModelState.Remove("Department");
            ModelState.Remove("Region");
            ModelState.Remove("Municipality");
            ModelState.Remove("City");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.LocationID))
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
           
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", location.CountryID);
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", location.DepartmentID);
            ViewBag.RegionID = new SelectList(_context.Regions, "RegionID", "RegionName", location.RegionID);
            ViewBag.MunicipalityID = new SelectList(_context.Municipalities.Where(m => m.RegionID == location.RegionID), "MunicipalityID", "MunicipalityName");
            ViewBag.CityID = new SelectList(_context.Cities, "CityID", "CityName", location.CityID);
            
            return View(location);
        }

        // GET: Locations/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .Include(l => l.City)
                .ThenInclude(x => x.Municipality)
                .ThenInclude(m => m.Region)
                .ThenInclude(r => r.Department)
                .ThenInclude(d => d.Country)
                .FirstOrDefaultAsync(m => m.LocationID == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Locations
                 .Include(l => l.City)
                 .ThenInclude(x => x.Municipality)
                 .ThenInclude(m => m.Region)
                 .ThenInclude(r => r.Department)
                 .ThenInclude(d => d.Country)
                 .FirstOrDefaultAsync(m => m.LocationID == id);

            if (location != null)
            {
                _context.Locations.Remove(location);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.LocationID == id);
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

        // Método para obtener Departamento según el país seleccionado
        public async Task<JsonResult> GetDepartments(int countryId)
        {
            var departments = await _context.Departments
                .Where(d => d.CountryID == countryId)
                .Select(d => new { id = d.DepartmentID, name = d.DepartmentName })
                .ToListAsync();
            return Json(departments);
        }

        // Método para obtener Región según el Departamento seleccionado
        public async Task<JsonResult> GetRegions(int departmentId)
        {
            var regions = await _context.Regions
                .Where(r => r.DepartmentID == departmentId)
                .Select(r => new { id = r.RegionID, name = r.RegionName })
                .ToListAsync();
            return Json(regions);
        }

        // Método para obtener Municipio según el la Región seleccionada
        public async Task<JsonResult> GetMunicipalities(int regionId)
        {
            var municipalities = await _context.Municipalities
                .Where(m => m.RegionID == regionId)
                .Select(m => new { id = m.MunicipalityID, name = m.MunicipalityName })
                .ToListAsync();
            return Json(municipalities);
        }

        // Método para obtener Ciudades según el Municipio seleccionado
        public async Task<JsonResult> GetCities(int municipalityId)
        {
            var cities = await _context.Cities
                .Where(c => c.MunicipalityID == municipalityId)
                .Select(c => new { id = c.CityID, name = c.CityName })
                .ToListAsync();
            return Json(cities);
        }


    }
}

