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
    public class shipmentsdriversController : Controller
    {
        private readonly VodilaContext _context;

        public shipmentsdriversController(VodilaContext context)
        {
            _context = context;
        }

        // GET: shipmentsdrivers
        public async Task<IActionResult> Index()
        {
            var vodilaContext = _context.shipmentsdrivers.Include(s => s.Driver).Include(s => s.Shipment);
            return View(await vodilaContext.ToListAsync());
        }

        // GET: shipmentsdrivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentsdriver = await _context.shipmentsdrivers
                .Include(s => s.Driver)
                .Include(s => s.Shipment)
                .FirstOrDefaultAsync(m => m.Shipmentid == id);
            if (shipmentsdriver == null)
            {
                return NotFound();
            }

            return View(shipmentsdriver);
        }

        // GET: shipmentsdrivers/Create
        public IActionResult Create()
        {
            ViewData["Driverid"] = new SelectList(_context.Useraccounts, "Id", "Firstname");
            ViewData["shipmentid"] = new SelectList(_context.shipments, "Id", "Id");
            return View();
        }

        // POST: shipmentsdrivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("shipmentid,Driverid")] Shipmentsdriver shipmentsdriver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipmentsdriver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Driverid"] = new SelectList(_context.Useraccounts, "Id", "Firstname", shipmentsdriver.Driverid);
            ViewData["shipmentid"] = new SelectList(_context.shipments, "Id", "Route.Departureplace", shipmentsdriver.Shipmentid);
            return View(shipmentsdriver);
        }

        // GET: shipmentsdrivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentsdriver = await _context.shipmentsdrivers.FindAsync(id);
            if (shipmentsdriver == null)
            {
                return NotFound();
            }
            ViewData["Driverid"] = new SelectList(_context.Useraccounts, "Id", "Firstname", shipmentsdriver.Driverid);
            ViewData["shipmentid"] = new SelectList(_context.shipments, "Id", "Id", shipmentsdriver.Shipmentid);
            return View(shipmentsdriver);
        }

        // POST: shipmentsdrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("shipmentid,Driverid")] Shipmentsdriver shipmentsdriver)
        {
            if (id != shipmentsdriver.Shipmentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipmentsdriver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!shipmentsdriverExists(shipmentsdriver.Shipmentid))
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
            ViewData["Driverid"] = new SelectList(_context.Useraccounts, "Id", "Firstname", shipmentsdriver.Driverid);
            ViewData["shipmentid"] = new SelectList(_context.shipments, "Id", "Id", shipmentsdriver.Shipmentid);
            return View(shipmentsdriver);
        }

        // GET: shipmentsdrivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentsdriver = await _context.shipmentsdrivers
                .Include(s => s.Driver)
                .Include(s => s.Shipment)
                .FirstOrDefaultAsync(m => m.Shipmentid == id);
            if (shipmentsdriver == null)
            {
                return NotFound();
            }

            return View(shipmentsdriver);
        }

        // POST: shipmentsdrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipmentsdriver = await _context.shipmentsdrivers.FindAsync(id);
            _context.shipmentsdrivers.Remove(shipmentsdriver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool shipmentsdriverExists(int id)
        {
            return _context.shipmentsdrivers.Any(e => e.Shipmentid == id);
        }
    }
}
