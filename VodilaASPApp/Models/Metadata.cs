
using System;
using System.ComponentModel.DataAnnotations;

namespace VodilaASPApp.Models
{
    public partial class CarMetadata
    {
        [Required]
        public string Name;
    }
    public partial class RouteMetadata
    {
        [StringLength(60)]
        [Display(Name = "Departure Place")]
        public string Departureplace;
        [StringLength(60)]
        [Display(Name = "Arrival Place")]
        public string Arrivalplace;
        public double Payment;
        public double Distance;
    }
    public partial class ShipmentMetadata
    {
        public double Bonus;
        [Display(Name = "Departure Time")]
        public DateTime? Departuretime;
        [Display(Name = "Arrival Time")]
        public DateTime? Arrivaltime;
        [Display(Name = "Prefered Departure Time")]
        public DateTime Prefereddeparturetime;
        [Display(Name = "Prefered Arrival Time")]
        public DateTime Preferedarrivaltime;
    }
    public partial class UseraccountMetadata
    {
        [StringLength(20)]
        [Display(Name = "First Name")]
        public string Firstname;
        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string Lastname;
        [StringLength(20)]
        public string Patronymic;
        [Display(Name = "Employment Date")]
        public DateTime Employmentdate;
        public int Position;
        [Required]
        [Display(Name = "Profile Image")]
        public byte[] Profileimage;
    }
    public partial class UserconfidentialMetadata
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
