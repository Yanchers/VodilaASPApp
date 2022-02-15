using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Shipmentsdriver
    {
        public int Shipmentid { get; set; }
        public int Driverid { get; set; }

        public virtual Useraccount Driver { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
}
