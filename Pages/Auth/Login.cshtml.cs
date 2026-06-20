using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto.Data;
using System.Linq;

namespace MyApp.Namespace
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost(string correo, string contrasena, string returnUrl)
        {
            var socio = _db.Socios
                .FirstOrDefault(s => s.CorreoElectronico == correo && s.Contrasena == contrasena);

            if (socio != null)
            {
                HttpContext.Session.SetString("SocioId", socio.NumSocio.ToString());
                HttpContext.Session.SetString("SocioNombre", socio.NombCompleto);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToPage("/Index");
            }

            ErrorMessage = "Correo o contraseña incorrectos.";
            return Page();
        }
    }
}