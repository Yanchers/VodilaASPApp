using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Shippment
    {
        public Shippment()
        {
            Shippmentsdrivers = new HashSet<Shippmentsdriver>();
        }

        public int Id { get; set; }
        public int Carid { get; set; }
        public int Routeid { get; set; }
        public double Bonus { get; set; }
        public DateTime Departuretime { get; set; }
        public DateTime Arrivaltime { get; set; }
        public DateTime Prefereddeparturetime { get; set; }
        public DateTime Preferedarrivaltime { get; set; }

        public virtual Car Car { get; set; }
        public virtual Route Route { get; set; }
        public virtual ICollection<Shippmentsdriver> Shippmentsdrivers { get; set; }
    }
}
