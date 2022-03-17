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

        public Shipment CurrentShipment { get; set; }
        
        public List<Shipment> Shipments { get; set; }

        public void OnGet()
        {
            var userid = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            Shipments = _context.Shipments
                .Include(s => s.Shipmentsdrivers)
                .Include(s => s.Car)
                .Include(s => s.Route)
                .Where(s => s.Shipmentsdrivers.Any(sd => sd.Driverid == userid)).ToList();
            CurrentShipment = Shipments.FirstOrDefault(s => !s.IsComplete);
        }
        public void OnPostCancel()
        {
            var userid = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            Shipments = _context.Shipments
                .Include(s => s.Shipmentsdrivers)
                .Include(s => s.Car)
                .Include(s => s.Route)
                .Where(s => s.Shipmentsdrivers.Any(sd => sd.Driverid == userid)).ToList();
            CurrentShipment = Shipments.FirstOrDefault(s => !s.IsComplete);
            if (CurrentShipment == null)
            {
                Page();
                return;
            }

            CurrentShipment.Departuretime = null;
            CurrentShipment.Shipmentsdrivers.Clear();
            _context.Update(CurrentShipment);
            _context.SaveChanges();

            RedirectToPage();
        }
        public void OnPostComplete()
        {
            var userid = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            Shipments = _context.Shipments
                .Include(s => s.Shipmentsdrivers)
                .Include(s => s.Car)
                .Include(s => s.Route)
                .Where(s => s.Shipmentsdrivers.Any(sd => sd.Driverid == userid)).ToList();
            CurrentShipment = Shipments.FirstOrDefault(s => !s.IsComplete);
            if (CurrentShipment == null)
            {
                Page();
                return;
            }

            CurrentShipment.IsComplete = true;
            CurrentShipment.Arrivaltime = DateTime.Now;
            if (CurrentShipment.Preferedarrivaltime < CurrentShipment.Arrivaltime)
                CurrentShipment.Bonus = (CurrentShipment.Preferedarrivaltime - CurrentShipment.Arrivaltime.Value).TotalHours * 10;
            _context.Update(CurrentShipment);
            _context.SaveChanges();

            RedirectToPage();
        }
    }
}
