using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VodilaASPApp.Models;

namespace VodilaASPApp.Pages
{
    public class LoginModel : PageModel
    {
        VodilaContext _context;

        public LoginModel(VodilaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Userconfidential UserConf { get; set; }
        public void OnGet()
        {
            UserConf = new Userconfidential();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var user = _context.Userconfidentials.First(u => u.Username == UserConf.Username && u.Password == UserConf.Password);
            if (user == null) return Page();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Sid, user.Userid.ToString()),
                new Claim("IsAccountant", "true"),
            };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);
            
            await HttpContext.SignInAsync("MyCookieAuth", principal);
            return RedirectToAction("Index");
        }
    }
}
