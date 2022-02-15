using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VodilaASPApp.Models;

namespace VodilaASPApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Userconfidential UserConf { get; set; }
        public void OnGet()
        {
            UserConf = new Userconfidential();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, UserConf.Username),
                new Claim("IsAccountant", "true"),
            };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);
            
            await HttpContext.SignInAsync("MyCookieAuth", principal);
            return RedirectToAction("Index");
        }
    }
}
