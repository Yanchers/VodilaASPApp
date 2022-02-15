using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VodilaASPApp.Models
{
    [ModelMetadataType(typeof(CarMetadata))]
    public partial class Car { }

    [ModelMetadataType(typeof(RouteMetadata))]
    public partial class Route { }

    [ModelMetadataType(typeof(ShipmentMetadata))]
    public partial class Shipment { }

    [ModelMetadataType(typeof(UseraccountMetadata))]
    public partial class Useraccount { }

    [ModelMetadataType(typeof(UserconfidentialMetadata))]
    public partial class Userconfidential { }
}
