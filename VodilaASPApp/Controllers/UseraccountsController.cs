using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VodilaASPApp.Models;

namespace VodilaASPApp.Controllers
{
    public class UseraccountsController : Controller
    {
        private readonly VodilaContext _context;

        public UseraccountsController(VodilaContext context)
        {
            _context = context;
        }

        // GET: Useraccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Useraccounts.ToListAsync());
        }

        // GET: Useraccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useraccount == null)
            {
                return NotFound();
            }

            return View(useraccount);
        }

        // GET: Useraccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Useraccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Patronymic,Employmentdate,Position,Profileimage")] Useraccount useraccount, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                using (BinaryReader reader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    useraccount.Profileimage = reader.ReadBytes((int)uploadedFile.Length);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(useraccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(useraccount);
        }

        // GET: Useraccounts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var useraccount = await _context.Useraccounts.FindAsync(id);
        //    if (useraccount == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(useraccount);
        //}

        // POST: Useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Patronymic,Employmentdate,Position,Profileimage")] Useraccount useraccount, IFormFile uploadedFile)
        {
            if (id != useraccount.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                        using (BinaryReader reader = new BinaryReader(uploadedFile.OpenReadStream()))
                        {
                            useraccount.Profileimage = reader.ReadBytes((int)uploadedFile.Length);
                        }
                    }
                    _context.Update(useraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(useraccount.Id))
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
            return View(useraccount);
        }

        // GET: Useraccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useraccount == null)
            {
                return NotFound();
            }

            return View(useraccount);
        }

        // POST: Useraccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var useraccount = await _context.Useraccounts.FindAsync(id);
            _context.Useraccounts.Remove(useraccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseraccountExists(int id)
        {
            return _context.Useraccounts.Any(e => e.Id == id);
        }
    }
}
