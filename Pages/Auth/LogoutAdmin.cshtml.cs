using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class LogoutAdminModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("AdminId");
            HttpContext.Session.Remove("AdminNombre");
            return RedirectToPage("/Index");
        }
    }
}