using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#nullable disable

namespace VodilaASPApp.Models
{
    public partial class Userconfidential
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual Useraccount User { get; set; }
    }
}
