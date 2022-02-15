using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using VodilaASPApp.Models;
using System;

namespace VodilaASPApp.Pages.Account
{
    public class ProfileModel : PageModel
    {
        VodilaContext _context;

        public ProfileModel(VodilaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shipment CurrentShipment { get; set; }
        
        public List<Shipment> Shipments { get; set; }

        public void OnGet()
        {
            var userid = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            var list = _context.Shipments
                .Include(s => s.Shipmentsdrivers)
                .Include(s => s.Car)
                .Include(s => s.Route)
                .Where(s => s.Shipmentsdrivers.Any(sd => sd.Driverid == userid));
            CurrentShipment = list.FirstOrDefault(s => !s.IsComplete);
            Shipments = list.ToList();
        }
        public void OnPostCancel()
        {
            CurrentShipment.Departuretime = null;
            _context.Update(CurrentShipment);
            _context.SaveChanges();

            RedirectToPage();
        }
        public void OnPostComplete()
        {
            CurrentShipment.IsComplete = true;
            CurrentShipment.Arrivaltime = DateTime.Now;
            _context.Update(CurrentShipment);
            _context.SaveChanges();

            RedirectToPage();
        }
    }
}
