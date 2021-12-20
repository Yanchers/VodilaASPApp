using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Car
    {
        public Car()
        {
            Shippments = new HashSet<Shippment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Shippment> Shippments { get; set; }
    }
}
