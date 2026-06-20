using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Pages.Admin;

public class GestionUsuariosModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Buscar { get; set; } = string.Empty;
    private readonly ApplicationDbContext _context;

    public GestionUsuariosModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Socio> Socios { get; set; } = new List<Socio>();

    public void OnGet()
    {
        var consulta = _context.Socios
            .Include(s => s.MatriculaCredencialNavigation)
            .Include(s => s.Telefonos)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(Buscar))
        {
            consulta = consulta.Where(s =>
                s.NombCompleto.Contains(Buscar) ||
                s.NumSocio.ToString().Contains(Buscar) ||
                s.CorreoElectronico.Contains(Buscar) ||
                s.Telefonos.Any(t => t.Numero.Contains(Buscar)));
        }

        Socios = consulta.ToList();
    }

    public IActionResult OnPost(
        string NombreCompleto,
        string CorreoElectronico,
        string Direccion,
        string Telefono,
        int NumeroCredencial,
        string Contraseña
        )
    {
        var credencial = new Credencial
        {
            Numero = NumeroCredencial
        };

        _context.Credencials.Add(credencial);
        _context.SaveChanges();

        var socio = new Socio
        {
            NombCompleto = NombreCompleto,
            CorreoElectronico = CorreoElectronico,
            Direccion = Direccion,
            MatriculaCredencial = credencial.MatriculaCredencial,
            Contrasena = Contraseña
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

        TempData["Exito"] = "Usuario agregado correctamente.";

        return RedirectToPage();
    }

    public IActionResult OnPostEditar(
        int NumSocio,
        string NombreCompleto,
        string CorreoElectronico,
        string Direccion,
        string Telefono,
        int NumeroCredencial,
        string Contrasena)
    {
        var socio = _context.Socios
            .Include(s => s.Telefonos)
            .Include(s => s.MatriculaCredencialNavigation)
            .FirstOrDefault(s => s.NumSocio == NumSocio);

        if (socio == null)
        {
            return NotFound();
        }

        socio.NombCompleto = NombreCompleto;
        socio.CorreoElectronico = CorreoElectronico;
        socio.Direccion = Direccion;
        socio.Contrasena = Contrasena;

        if (socio.MatriculaCredencialNavigation != null)
        {
            socio.MatriculaCredencialNavigation.Numero = NumeroCredencial;
        }

        var telefono = socio.Telefonos.FirstOrDefault();

        if (telefono != null)
        {
            telefono.Numero = Telefono;
        }
        else
        {
            _context.Telefonos.Add(new Telefono
            {
                Numero = Telefono,
                NumSocio = socio.NumSocio
            });
        }

        _context.SaveChanges();

        TempData["Exito"] = "Usuario actualizado correctamente.";

        return RedirectToPage();
    }

    public IActionResult OnPostEliminar(int NumSocio)
    {
        var socio = _context.Socios
            .Include(s => s.Telefonos)
            .Include(s => s.MatriculaCredencialNavigation)
            .Include(s => s.Prestamos)
            .FirstOrDefault(s => s.NumSocio == NumSocio);

        if (socio == null)
        {
            return NotFound();
        }

        var tienePrestamos = _context.Prestamos
            .Any(p => p.NumSocio == NumSocio);

        if (tienePrestamos)
        {
            TempData["Error"] =
                "No se puede eliminar este usuario porque tiene préstamos registrados.";

            return RedirectToPage();
        }

        var credencial = socio.MatriculaCredencialNavigation;

        foreach (var telefono in socio.Telefonos.ToList())
        {
            _context.Telefonos.Remove(telefono);
        }

        _context.Socios.Remove(socio);
        _context.SaveChanges();

        if (credencial != null)
        {
            _context.Credencials.Remove(credencial);
            _context.SaveChanges();
        }

        TempData["Exito"] = "Usuario eliminado correctamente.";

        return RedirectToPage();
    }
}