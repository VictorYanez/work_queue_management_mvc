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
    public class UnitsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public UnitsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Units
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Units.Include(u => u.Area);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .Include(u => u.Area)
                .FirstOrDefaultAsync(m => m.UnitID == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Units/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AreaID = new SelectList(_context.Areas, "AreaID", "AreaName");
            return View();
        }

        // POST: Units/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitID,UnitName,UnitDescription,AreaID,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] Unit unit)
        {
            // Remover propiedades de navegación del ModelState para evitar validación innecesaria
            ModelState.Remove("Area");

            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AreaID = new SelectList(_context.Areas, "AreaID", "AreaName", unit.AreaID);
            return View(unit);
        }

        // GET: Units/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xunit = await _context.Units.FindAsync(id);
            var yunit = await _context.Units.AsNoTracking()
                .FirstOrDefaultAsync(d => d.UnitID == id);

            var unit = await _context.Units
                .Include(r => r.Area)
                .FirstOrDefaultAsync(m => m.UnitID == id);

            if (unit == null)
            {
                return NotFound();
            }
            ViewBag.AreaID = new SelectList(_context.Areas, "AreaID", "AreaName", unit.AreaID);
            return View(unit);
        }

        // POST: Units/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UnitID,UnitName,UnitDescription,AreaID,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,RowVersion")] Unit unit)
        {
            if (id != unit.UnitID)
            {
                return NotFound();
            }


            ModelState.Remove("Area");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.UnitID))
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

            ViewBag.AreaID = new SelectList(_context.Areas, "AreaID", "AreaName", unit.AreaID);

            return View(unit);
        }

        // GET: Units/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .Include(u => u.Area)
                .FirstOrDefaultAsync(m => m.UnitID == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Funciones Adicionales 
        private bool UnitExists(int id)
        {
            return _context.Units.Any(e => e.UnitID == id);
        }

        public async Task<JsonResult> GetUnits(int areaId)
        {
            var units = await _context.Units.Where(u => u.AreaID == areaId).ToListAsync();
            return Json(new SelectList(units, "UnitID", "UnitName"));
        }


    }
}
