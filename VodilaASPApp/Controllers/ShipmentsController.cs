using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VodilaASPApp.Models;
using VodilaASPApp.Models.ViewModels;

namespace VodilaASPApp.Controllers
{
    public class shipmentsController : Controller
    {
        private readonly VodilaContext _context;

        public shipmentsController(VodilaContext context)
        {
            _context = context;
        }

        // GET: shipments
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var vodilaContext = _context.shipments.Include(s => s.Car).Include(s => s.Route);
            return View(await vodilaContext.ToListAsync());
        }

        // GET: shipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.shipments
                .Include(s => s.Car)
                .Include(s => s.Route)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // GET: shipments/Create
        public IActionResult Create(int? routeid)
        {
            ViewData["Carid"] = new SelectList(_context.Cars, "Id", "Name");
            var routes = _context.Routes;
            if (routeid != null)
                ViewData["Routeid"] = new SelectList(routes, "Id", "Arrivalplace", routeid);
            else
                ViewData["Routeid"] = new SelectList(routes, "Id", "Arrivalplace");

            ViewData["Useraccounts"] = new List<AssignedDriverData>(_context.Useraccounts.Select(ua=>new AssignedDriverData
            {
                UseraccountId = ua.Id,
                FullName = $"{ua.Lastname} {ua.Firstname}",
            }));
            return View();
        }

        // POST: shipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Carid,Routeid,Bonus,Departuretime,Arrivaltime,Prefereddeparturetime,Preferedarrivaltime")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "Id", "Name", shipment.Carid);
            ViewData["Routeid"] = new SelectList(_context.Routes, "Id", "Arrivalplace", shipment.Routeid);
            return View(shipment);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Carid,Routeid,Bonus,Departuretime,Arrivaltime,Prefereddeparturetime,Preferedarrivaltime")] Shipment shipment, string[] selectedDrivers)
        //{
        //    if (selectedDrivers != null)
        //    {
        //        shipment.Shipmentsdrivers = new List<Shipmentsdriver>();
        //        foreach (string driver in selectedDrivers)
        //            shipment.Shipmentsdrivers.Add(new Shipmentsdriver
        //            {
        //                Driverid = int.Parse(driver),
        //                Shipmentid = shipment.Id
        //            });
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(shipment);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Carid"] = new SelectList(_context.Cars, "Id", "Name", shipment.Carid);
        //    ViewData["Routeid"] = new SelectList(_context.Routes, "Id", "Arrivalplace", shipment.Routeid);
        //    return View(shipment);
        //}

        // GET: shipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.shipments
                .Include(s=>s.Car)
                .Include(s=>s.Route)
                .Include(s=>s.Shipmentsdrivers)
                .FirstOrDefaultAsync(s=>s.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }
            ViewData["Carid"] = new SelectList(_context.Cars, "Id", "Name", shipment.Carid);
            ViewData["Routeid"] = new SelectList(_context.Routes, "Id", "Arrivalplace", shipment.Routeid);

            var drivers = shipment.Shipmentsdrivers.Select(sd=>sd.Driverid);
            ViewData["Useraccounts"] = new List<AssignedDriverData>(_context.Useraccounts.Select(ua => new AssignedDriverData
            {
                UseraccountId = ua.Id,
                FullName = $"{ua.Lastname} {ua.Firstname}",
                Assigned = drivers.Contains(ua.Id)
            }));
            return View(shipment);
        }

        // POST: shipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Carid,Routeid,Bonus,Departuretime,Arrivaltime,Prefereddeparturetime,Preferedarrivaltime")] Shipment shipment, string[] selectedDrivers)
        {
            if (id != shipment.Id)
            {
                return NotFound();
            }

            if (selectedDrivers != null)
            {
                _context.RemoveRange(shipment.Shipmentsdrivers);
                shipment.Shipmentsdrivers.Clear();
                foreach (string driver in selectedDrivers)
                {
                    var user = await _context.Useraccounts.FirstAsync(ua => ua.Id == int.Parse(driver));
                    shipment.Shipmentsdrivers.Add(new Shipmentsdriver
                    {
                        Driverid = user.Id,
                        Shipmentid = shipment.Id,
                    });
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!shipmentExists(shipment.Id))
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
            ViewData["Carid"] = new SelectList(_context.Cars, "Id", "Name", shipment.Carid);
            ViewData["Routeid"] = new SelectList(_context.Routes, "Id", "Arrivalplace", shipment.Routeid);
            return View(shipment);
        }

        // GET: shipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.shipments
                .Include(s => s.Car)
                .Include(s => s.Route)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // POST: shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipment = await _context.shipments.FindAsync(id);
            _context.shipments.Remove(shipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool shipmentExists(int id)
        {
            return _context.shipments.Any(e => e.Id == id);
        }
    }
}
