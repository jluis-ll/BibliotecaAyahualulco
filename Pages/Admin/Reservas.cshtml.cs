using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Pages.Admin;

public class ReservasModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Buscar { get; set; } = string.Empty;
    public IList<Socio> Socios { get; set; } = new List<Socio>();
    public IList<Libro> Libros { get; set; } = new List<Libro>();
    private readonly ApplicationDbContext _context;

    public ReservasModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Reserva> Reservas { get; set; } = new List<Reserva>();

    public void OnGet()
    {
        var consulta = _context.Reservas
    .Include(r => r.NumSocioNavigation)
    .Include(r => r.FolioLibroNavigation)
    .AsQueryable();

        if (!string.IsNullOrWhiteSpace(Buscar))
        {
            consulta = consulta.Where(r =>
                r.IdReserva.ToString().Contains(Buscar) ||
                r.NumSocioNavigation.NombCompleto.Contains(Buscar) ||
                r.FolioLibroNavigation.Nombre.Contains(Buscar));
        }

        Reservas = consulta.ToList();

        Socios = _context.Socios.ToList();
        Libros = _context.Libros
                .Where(l => l.NumeroCopias == 0)
                .ToList();
    }

    public IActionResult OnPost(int NumSocio, int FolioLibro)
    {
        var libro = _context.Libros.FirstOrDefault(l => l.FolioLibro == FolioLibro);

        if (libro == null)
        {
            return NotFound();
        }

        if (libro.NumeroCopias > 0)
        {
            TempData["Error"] = "No puedes reservar un libro con copias disponibles.";
            return RedirectToPage();
        }

        var existeReserva = _context.Reservas.Any(r =>
            r.NumSocio == NumSocio &&
            r.FolioLibro == FolioLibro);

        if (existeReserva)
        {
            TempData["Error"] = "Este usuario ya tiene esta reserva.";
            return RedirectToPage();
        }

        var reserva = new Reserva
        {
            NumSocio = NumSocio,
            FolioLibro = FolioLibro,
            FechaReserva = DateTime.Now
        };

        _context.Reservas.Add(reserva);
        _context.SaveChanges();

        TempData["Exito"] = "Reserva registrada correctamente.";

        return RedirectToPage();
    }

    public IActionResult OnPostEliminar(int IdReserva)
    {
        var reserva = _context.Reservas
            .FirstOrDefault(r => r.IdReserva == IdReserva);

        if (reserva == null)
        {
            return NotFound();
        }

        _context.Reservas.Remove(reserva);
        _context.SaveChanges();

        TempData["Exito"] = "Reserva eliminada correctamente.";

        return RedirectToPage();
    }

    public IActionResult OnPostEditar(int IdReserva, int FolioLibro)
    {
        var reserva = _context.Reservas
            .FirstOrDefault(r => r.IdReserva == IdReserva);

        if (reserva == null)
        {
            return NotFound();
        }

        var libro = _context.Libros.FirstOrDefault(l => l.FolioLibro == FolioLibro);

        if (libro != null && libro.NumeroCopias > 0)
        {
            TempData["Error"] = "No puedes asignar un libro con copias disponibles.";
            return RedirectToPage();
        }

        reserva.FolioLibro = FolioLibro;

        _context.SaveChanges();

        TempData["Exito"] = "Reserva actualizada correctamente.";

        return RedirectToPage();
    }
}