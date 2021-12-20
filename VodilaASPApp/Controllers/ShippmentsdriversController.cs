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
    public class ShippmentsdriversController : Controller
    {
        private readonly VodilaContext _context;

        public ShippmentsdriversController(VodilaContext context)
        {
            _context = context;
        }

        // GET: Shippmentsdrivers
        public async Task<IActionResult> Index()
        {
            var vodilaContext = _context.Shippmentsdrivers.Include(s => s.Driver).Include(s => s.Shippment);
            return View(await vodilaContext.ToListAsync());
        }

        // GET: Shippmentsdrivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippmentsdriver = await _context.Shippmentsdrivers
                .Include(s => s.Driver)
                .Include(s => s.Shippment)
                .FirstOrDefaultAsync(m => m.Shippmentid == id);
            if (shippmentsdriver == null)
            {
                return NotFound();
            }

            return View(shippmentsdriver);
        }

        // GET: Shippmentsdrivers/Create
        public IActionResult Create()
        {
            ViewData["Driverid"] = new SelectList(_context.Useraccounts, "Id", "Firstname");
            ViewData["Shippmentid"] = new SelectList(_context.Shippments, "Id", "Id");
            return View();
        }

        // POST: Shippmentsdrivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Shippmentid,Driverid")] Shippmentsdriver shippmentsdriver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippmentsdriver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Driverid"] = new SelectList(_context.Useraccounts, "Id", "Firstname", shippmentsdriver.Driverid);
            ViewData["Shippmentid"] = new SelectList(_context.Shippments, "Id", "Id", shippmentsdriver.Shippmentid);
            return View(shippmentsdriver);
        }

        // GET: Shippmentsdrivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippmentsdriver = await _context.Shippmentsdrivers.FindAsync(id);
            if (shippmentsdriver == null)
            {
                return NotFound();
            }
            ViewData["Driverid"] = new SelectList(_context.Useraccounts, "Id", "Firstname", shippmentsdriver.Driverid);
            ViewData["Shippmentid"] = new SelectList(_context.Shippments, "Id", "Id", shippmentsdriver.Shippmentid);
            return View(shippmentsdriver);
        }

        // POST: Shippmentsdrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Shippmentid,Driverid")] Shippmentsdriver shippmentsdriver)
        {
            if (id != shippmentsdriver.Shippmentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippmentsdriver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippmentsdriverExists(shippmentsdriver.Shippmentid))
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
            ViewData["Driverid"] = new SelectList(_context.Useraccounts, "Id", "Firstname", shippmentsdriver.Driverid);
            ViewData["Shippmentid"] = new SelectList(_context.Shippments, "Id", "Id", shippmentsdriver.Shippmentid);
            return View(shippmentsdriver);
        }

        // GET: Shippmentsdrivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippmentsdriver = await _context.Shippmentsdrivers
                .Include(s => s.Driver)
                .Include(s => s.Shippment)
                .FirstOrDefaultAsync(m => m.Shippmentid == id);
            if (shippmentsdriver == null)
            {
                return NotFound();
            }

            return View(shippmentsdriver);
        }

        // POST: Shippmentsdrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippmentsdriver = await _context.Shippmentsdrivers.FindAsync(id);
            _context.Shippmentsdrivers.Remove(shippmentsdriver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippmentsdriverExists(int id)
        {
            return _context.Shippmentsdrivers.Any(e => e.Shippmentid == id);
        }
    }
}
