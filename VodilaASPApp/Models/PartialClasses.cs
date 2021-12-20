using System.ComponentModel.DataAnnotations;

namespace VodilaASPApp.Models
{
    [MetadataType(typeof(CarMetadata))]
    public partial class Car { }

    [MetadataType(typeof(RouteMetadata))]
    public partial class Route { }

    [MetadataType(typeof(ShippmentMetadata))]
    public partial class Shippment { }

    [MetadataType(typeof(UseraccountMetadata))]
    public partial class Useraccount { }

    [MetadataType(typeof(UserconfidentialMetadata))]
    public partial class Userconfidential{ }
}
