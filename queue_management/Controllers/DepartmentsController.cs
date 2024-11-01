using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using queue_management.Data;
using queue_management.Models;

namespace queue_management.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public DepartmentsController(ApplicationDBContext context)
        {
            _context = context;
        }

        //Endpoint para la visualización del Listado 
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Departments.Include(d => d.Country);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Departments/Details/5
        //Endpoint para la visualización de Detalles del registro  
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound"); 
            }

            var department = await _context.Departments
                .Include(d => d.Country)
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (department == null)
            {
                return View("NotFound");
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName");
            return View();
        }

        //Endpoint para la creación del Listado 
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Departments/Create

        public async Task<IActionResult> Create([Bind("DepartmentName,CountryID")] Department department)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine(error); // O usa un logger para guardar estos mensajes
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", department.CountryID);

            return View(department); 
        }

        // GET: Para visualizar la información del campo a Editar  
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xdepartment = await _context.Departments.FindAsync(id);
            var department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.DepartmentID == id);

            if (department == null)
            {
                return NotFound();
            }

            ViewBag.CountryID = new SelectList(_context.Countries, "CountryID", "CountryName", department.CountryID);
            //ViewBag.CountryID = await _context.Countries.Select(m => new SelectListItem { Value = m.CountryID.ToString(), Text = m.CountryName }).ToListAsync();
            return View(department);
        }

        // POST: Departments/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentID,DepartmentName,CountryID,RowVersion")] Department department)
        {

            if (id != department.DepartmentID)
            {
                return RedirectToAction(nameof(NotFound), "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentID))
                    {
                        return RedirectToAction(nameof(NotFound), "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Countries = new SelectList(await _context.Countries.ToListAsync(), "CountryID", "CountryName", department.CountryID);
            ViewBag.Departments = new SelectList(await _context.Departments.Where(d => d.CountryID == department.CountryID).ToListAsync(), "DepartmentID", "DepartmentName", department.DepartmentID);

            return View(department);
        }



        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.Country)
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // POST: Funciones Adicionales 
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentID == id);
        }

        public async Task<JsonResult> GetDepartments(int countryId)
        {
            var departments = await _context.Departments.Where(d => d.CountryID == countryId).ToListAsync();
            return Json(new SelectList(departments, "DepartmentID", "DepartmentName"));
        }
    }
}
