using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Route
    {
        public Route()
        {
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string Departureplace { get; set; }
        public string Arrivalplace { get; set; }
        public double Payment { get; set; }
        public double Distance { get; set; }
        public bool RequireSecondDriver { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
