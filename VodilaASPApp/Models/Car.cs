using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Car
    {
        public Car()
        {
            shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Shipment> shipments { get; set; }
    }
}
