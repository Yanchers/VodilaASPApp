using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Useraccount
    {
        public Useraccount()
        {
            shipmentsdrivers = new HashSet<Shipmentsdriver>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public DateTime Employmentdate { get; set; }
        public int Position { get; set; }
        public byte[] Profileimage { get; set; }

        public virtual ICollection<Shipmentsdriver> shipmentsdrivers { get; set; }


        public string FullName => $"{Lastname} {Firstname[0]}. {(Patronymic == null ? "" : Patronymic[0] + ".")}";
    }
}
