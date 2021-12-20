using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Shippmentsdriver
    {
        public int Shippmentid { get; set; }
        public int Driverid { get; set; }

        public virtual Useraccount Driver { get; set; }
        public virtual Shippment Shippment { get; set; }
    }
}
