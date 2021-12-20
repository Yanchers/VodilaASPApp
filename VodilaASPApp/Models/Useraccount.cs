using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Useraccount
    {
        public Useraccount()
        {
            Shippmentsdrivers = new HashSet<Shippmentsdriver>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Employmentdate { get; set; }
        public int Position { get; set; }
        public string Profileimage { get; set; }

        public virtual ICollection<Shippmentsdriver> Shippmentsdrivers { get; set; }
    }
}
