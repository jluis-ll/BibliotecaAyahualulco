using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Pages.Libros;

public class DetalleLibroModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetalleLibroModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Libro Libro { get; set; } = null!;

    public IActionResult OnGet(int id)
    {
        var libro = _context.Libros
            .Include(l => l.IdEditorialNavigation)
            .Include(l => l.IdUbicacionNavigation)
                .ThenInclude(u => u.IdTemaNavigation)
            .Include(l => l.IdUbicacionNavigation)
                .ThenInclude(u => u.IdPasilloNavigation)
            .Include(l => l.IdAutors)
            .FirstOrDefault(l => l.FolioLibro == id);

        if (libro == null)
        {
            return NotFound();
        }

        Libro = libro;

        return Page();
    }

    public IActionResult OnPostReservar(int FolioLibro)
    {
        var socioId = HttpContext.Session.GetString("SocioId");

        if (string.IsNullOrEmpty(socioId))
        {
            return RedirectToPage("/Auth/Login", new
            {
                returnUrl = $"/Libros/DetalleLibro/{FolioLibro}"
            });
        }

        int numSocio = int.Parse(socioId);
        var libro = _context.Libros
            .FirstOrDefault(l => l.FolioLibro == FolioLibro);

        if (libro == null)
        {
            return NotFound();
        }

        if (libro.NumeroCopias > 0)
        {
            TempData["Error"] =
                "Este libro tiene copias disponibles, puedes solicitar préstamo.";

            return RedirectToPage("/Libros/DetalleLibro", new { id = FolioLibro });
        }

        var reservaExistente = _context.Reservas
            .Any(r => r.NumSocio == numSocio &&
                      r.FolioLibro == FolioLibro);

        if (reservaExistente)
        {
            TempData["Error"] =
                "Ya tienes una reserva registrada para este libro.";

            return RedirectToPage("/Libros/DetalleLibro", new { id = FolioLibro });
        }

        var reserva = new Reserva
        {
            NumSocio = numSocio,
            FolioLibro = FolioLibro,
            FechaReserva = DateTime.Now
        };

        _context.Reservas.Add(reserva);
        _context.SaveChanges();

        TempData["Exito"] =
            "Reserva registrada correctamente.";

        return RedirectToPage("/Libros/DetalleLibro", new { id = FolioLibro });
    }

    public IActionResult OnPostSolicitarPrestamo(int FolioLibro)
    {
        var socioId = HttpContext.Session.GetString("SocioId");

        if (string.IsNullOrEmpty(socioId))
        {
            return RedirectToPage("/Auth/Login", new
            {
                returnUrl = $"/Libros/DetalleLibro/{FolioLibro}"
            });
        }

        int numSocio = int.Parse(socioId);

        var libro = _context.Libros
            .FirstOrDefault(l => l.FolioLibro == FolioLibro);

        if (libro == null)
        {
            return NotFound();
        }

        if (libro.NumeroCopias <= 0)
        {
            TempData["Error"] =
                "No hay copias disponibles. Puedes reservar este libro.";

            return RedirectToPage("/Libros/DetalleLibro", new { id = FolioLibro });
        }

        var prestamoExistente = _context.Prestamos
            .Any(p => p.NumSocio == numSocio &&
                      p.FolioLibro == FolioLibro &&
                      p.EstatusPrestamo != "Entregado");

        if (prestamoExistente)
        {
            TempData["Error"] =
                "Ya tienes una solicitud o préstamo activo para este libro.";

            return RedirectToPage("/Libros/DetalleLibro", new { id = FolioLibro });
        }

        var prestamo = new Prestamo
        {
            NumSocio = numSocio
            ,
            FolioLibro = FolioLibro,
            FechaInicio = DateTime.Now,
            FechaEntrega = DateTime.Now.AddDays(7),
            EstatusPrestamo = "Solicitado",
            IdBibliotecario = null
        };

        _context.Prestamos.Add(prestamo);

        libro.NumeroCopias--;

        _context.SaveChanges();

        TempData["Exito"] =
            "Solicitud de préstamo registrada correctamente.";

        return RedirectToPage("/Libros/DetalleLibro", new { id = FolioLibro });
    }
}