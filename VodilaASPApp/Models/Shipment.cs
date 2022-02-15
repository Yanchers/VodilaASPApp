using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            Shipmentsdrivers = new HashSet<Shipmentsdriver>();
        }

        public int Id { get; set; }
        public int Carid { get; set; }
        public int Routeid { get; set; }
        public double Bonus { get; set; }
        public DateTime? Departuretime { get; set; }
        public DateTime? Arrivaltime { get; set; }
        public DateTime Prefereddeparturetime { get; set; }
        public DateTime Preferedarrivaltime { get; set; }
        public bool IsComplete { get; set; }

        public virtual Car Car { get; set; }
        public virtual Route Route { get; set; }
        public virtual ICollection<Shipmentsdriver> Shipmentsdrivers { get; set; }
    }
}
