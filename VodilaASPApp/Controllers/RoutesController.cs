﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VodilaASPApp.Models;

namespace VodilaASPApp.Controllers
{
    public class RoutesController : Controller
    {
        private readonly VodilaContext _context;

        public RoutesController(VodilaContext context)
        {
            _context = context;
        }

        // GET: Routes
        public async Task<IActionResult> Index(string sortOrder, string searchQuery)
        {
            ViewBag.DepartureplaceSort = string.IsNullOrEmpty(sortOrder) ? "departureplace_desc" : "";
            ViewBag.ArrivalplaceSort = sortOrder == "arrivalplace" ? "arrivalplace_desc" : "arrivalplace";

            IEnumerable<Route> routes = _context.Routes.AsEnumerable();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                routes = routes.Where(r => r.Arrivalplace.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) 
                || r.Departureplace.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }
            switch (sortOrder)
            {
                case "departureplace_desc":
                    routes = routes.OrderByDescending(r => r.Departureplace);
                    break;
                case "arrivalplace":
                    routes = routes.OrderBy(r => r.Arrivalplace);
                    break;
                case "arrivalplace_desc":
                    routes = routes.OrderByDescending(r => r.Arrivalplace);
                    break;
                default:
                    routes = routes.OrderBy(r => r.Departureplace);
                    break;
            }

            return View(routes.ToList());
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Departureplace,Arrivalplace,Payment,Distance")] Route route)
        {
            if (ModelState.IsValid)
            {
                route.Payment = new Random().Next(2999, 18000);
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Departureplace,Arrivalplace,Payment,Distance")] Route route)
        {
            if (id != route.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.Id))
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
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.Id == id);
        }
    }
}
