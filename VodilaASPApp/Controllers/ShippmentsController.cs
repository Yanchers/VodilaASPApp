using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VodilaASPApp.Models;

namespace VodilaASPApp.Controllers
{
    public class ShippmentsController : Controller
    {
        private readonly VodilaContext _context;

        public ShippmentsController(VodilaContext context)
        {
            _context = context;
        }

        // GET: Shippments
        public async Task<IActionResult> Index()
        {
            var vodilaContext = _context.Shippments.Include(s => s.Car).Include(s => s.Route);
            return View(await vodilaContext.ToListAsync());
        }

        // GET: Shippments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippment = await _context.Shippments
                .Include(s => s.Car)
                .Include(s => s.Route)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippment == null)
            {
                return NotFound();
            }

            return View(shippment);
        }

        // GET: Shippments/Create
        public IActionResult Create()
        {
            ViewData["Carid"] = new SelectList(_context.Cars, "Id", "Name");
            ViewData["Routeid"] = new SelectList(_context.Routes, "Id", "Arrivalplace");
            return View();
        }

        // POST: Shippments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Carid,Routeid,Bonus,Departuretime,Arrivaltime,Prefereddeparturetime,Preferedarrivaltime")] Shippment shippment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "Id", "Name", shippment.Carid);
            ViewData["Routeid"] = new SelectList(_context.Routes, "Id", "Arrivalplace", shippment.Routeid);
            return View(shippment);
        }

        // GET: Shippments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippment = await _context.Shippments.FindAsync(id);
            if (shippment == null)
            {
                return NotFound();
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "Id", "Name", shippment.Carid);
            ViewData["Routeid"] = new SelectList(_context.Routes, "Id", "Arrivalplace", shippment.Routeid);
            return View(shippment);
        }

        // POST: Shippments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Carid,Routeid,Bonus,Departuretime,Arrivaltime,Prefereddeparturetime,Preferedarrivaltime")] Shippment shippment)
        {
            if (id != shippment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippmentExists(shippment.Id))
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
            ViewData["Carid"] = new SelectList(_context.Cars, "Id", "Name", shippment.Carid);
            ViewData["Routeid"] = new SelectList(_context.Routes, "Id", "Arrivalplace", shippment.Routeid);
            return View(shippment);
        }

        // GET: Shippments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippment = await _context.Shippments
                .Include(s => s.Car)
                .Include(s => s.Route)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippment == null)
            {
                return NotFound();
            }

            return View(shippment);
        }

        // POST: Shippments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippment = await _context.Shippments.FindAsync(id);
            _context.Shippments.Remove(shippment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippmentExists(int id)
        {
            return _context.Shippments.Any(e => e.Id == id);
        }
    }
}
