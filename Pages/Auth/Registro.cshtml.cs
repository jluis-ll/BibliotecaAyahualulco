using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto.Data;
using Proyecto.Models;

namespace MyApp.Namespace
{
    public class RegistroModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegistroModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string? ErrorMessage { get; set; }
        [TempData]
        public string? ExitoMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(
            string NombreCompleto,
            string Direccion,
            string Telefono,
            string CorreoElectronico,
            string Contrasena)
        {
            var correoExiste = _context.Socios
                .Any(s => s.CorreoElectronico == CorreoElectronico);

            if (correoExiste)
            {
                ErrorMessage = "Ya existe un usuario registrado con ese correo.";
                return Page();
            }

            var credencial = new Credencial
            {
                Numero = new Random().Next(10000, 99999)
            };

            _context.Credencials.Add(credencial);
            _context.SaveChanges();

            var socio = new Socio
            {
                NombCompleto = NombreCompleto,
                Direccion = Direccion,
                CorreoElectronico = CorreoElectronico,
                Contrasena = Contrasena,
                MatriculaCredencial = credencial.MatriculaCredencial
            };

            _context.Socios.Add(socio);
            _context.SaveChanges();

            var telefono = new Telefono
            {
                Numero = Telefono,
                NumSocio = socio.NumSocio
            };

            _context.Telefonos.Add(telefono);
            _context.SaveChanges();

            ExitoMessage =
            $"Registro realizado correctamente. Tu número de credencial es: {credencial.Numero}. Ya puedes iniciar sesión.";

            return RedirectToPage();
        }
    }
}