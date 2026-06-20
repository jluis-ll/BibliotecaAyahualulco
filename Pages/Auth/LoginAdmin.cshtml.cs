using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto.Data;
using System.Linq;

namespace MyApp.Namespace
{
    public class LoginAdminModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public LoginAdminModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost(string correo, string contrasena)
        {
            var bibliotecario = _db.Bibliotecarios
                .FirstOrDefault(b => b.CorreoElectronico == correo && b.Contrasena == contrasena);

            if (bibliotecario != null)
            {
                HttpContext.Session.SetString("AdminId", bibliotecario.IdBibliotecario.ToString());
                HttpContext.Session.SetString("AdminNombre", bibliotecario.Nombre);
                return RedirectToPage("/Admin/PanelAdmin");
            }

            ErrorMessage = "Correo o contraseña incorrectos.";
            return Page();
        }
    }
}