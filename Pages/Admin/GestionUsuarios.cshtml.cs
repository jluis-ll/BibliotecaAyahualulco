using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Pages.Admin;

public class GestionUsuariosModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public GestionUsuariosModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Socio> Socios { get; set; } = new List<Socio>();

    public void OnGet()
    {
        Socios = _context.Socios
            .Include(s => s.MatriculaCredencialNavigation)
            .Include(s => s.Telefonos)
            .ToList();
    }

    public IActionResult OnPost(
    string NombreCompleto,
    string CorreoElectronico,
    string Direccion,
    string Telefono,
    int NumeroCredencial)
    {
        // ===== CREDENCIAL =====

        var credencial = new Credencial
        {
            Numero = NumeroCredencial
        };

        _context.Credencials.Add(credencial);

        _context.SaveChanges();

        // ===== SOCIO =====

        var socio = new Socio
        {
            NombCompleto = NombreCompleto,
            CorreoElectronico = CorreoElectronico,
            Direccion = Direccion,
            MatriculaCredencial = credencial.MatriculaCredencial
        };

        _context.Socios.Add(socio);

        _context.SaveChanges();

        // ===== TELEFONO =====

        var telefono = new Telefono
        {
            Numero = Telefono,
            NumSocio = socio.NumSocio
        };

        _context.Telefonos.Add(telefono);

        _context.SaveChanges();

        return RedirectToPage();
    }

    public IActionResult OnPostEditar(
        int NumSocio,
        string NombreCompleto,
        string CorreoElectronico,
        string Direccion,
        string Telefono,
        int NumeroCredencial)
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
            var nuevoTelefono = new Telefono
            {
                Numero = Telefono,
                NumSocio = socio.NumSocio
            };

            _context.Telefonos.Add(nuevoTelefono);
        }

        _context.SaveChanges();

        return RedirectToPage();
    }

    public IActionResult OnPostEliminar(int NumSocio)
    {
        var socio = _context.Socios
            .Include(s => s.Telefonos)
            .Include(s => s.MatriculaCredencialNavigation)
            .FirstOrDefault(s => s.NumSocio == NumSocio);

        if (socio == null)
        {
            return NotFound();
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

        return RedirectToPage();
    }
}