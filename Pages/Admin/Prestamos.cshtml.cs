using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Pages.Admin;

public class PrestamosModel : PageModel
{
    public IList<Socio> Socios { get; set; } = new List<Socio>();
    public IList<Libro> Libros { get; set; } = new List<Libro>();
    public IList<Bibliotecario> Bibliotecarios { get; set; } = new List<Bibliotecario>();

    private readonly ApplicationDbContext _context;

    public PrestamosModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public void OnGet()
    {
        Prestamos = _context.Prestamos
            .Include(p => p.FolioLibroNavigation)
            .Include(p => p.NumSocioNavigation)
            .Include(p => p.IdBibliotecarioNavigation)
            .ToList();

        Socios = _context.Socios.ToList();

        Libros = _context.Libros
            .Where(l => l.NumeroCopias > 0)
            .ToList();

        Bibliotecarios = _context.Bibliotecarios.ToList();
    }

    public IActionResult OnPost(
        int NumSocio,
        int FolioLibro,
        int? IdBibliotecario)
    {
        var libro = _context.Libros
            .FirstOrDefault(l => l.FolioLibro == FolioLibro);

        if (libro == null)
        {
            return NotFound();
        }

        if (libro.NumeroCopias <= 0)
        {
            TempData["Error"] = "No hay copias disponibles.";
            return RedirectToPage();
        }

        var yaTiene = _context.Prestamos.Any(p =>
            p.NumSocio == NumSocio &&
            p.FolioLibro == FolioLibro &&
            p.EstatusPrestamo == "Prestado");

        if (yaTiene)
        {
            TempData["Error"] = "El usuario ya tiene este libro.";
            return RedirectToPage();
        }

        var activos = _context.Prestamos.Count(p =>
            p.NumSocio == NumSocio &&
            p.EstatusPrestamo == "Prestado");

        if (activos >= 3)
        {
            TempData["Error"] = "El usuario ya alcanzó el límite de préstamos.";
            return RedirectToPage();
        }

        var reserva = _context.Reservas.FirstOrDefault(r =>
            r.NumSocio == NumSocio &&
            r.FolioLibro == FolioLibro);

        var prestamo = new Prestamo
        {
            NumSocio = NumSocio,
            FolioLibro = FolioLibro,
            FechaInicio = DateTime.Now,
            FechaEntrega = DateTime.Now.AddDays(7),

            EstatusPrestamo = "Prestado",
            IdBibliotecario = IdBibliotecario
        };

        _context.Prestamos.Add(prestamo);

        libro.NumeroCopias--;

        if (reserva != null)
        {
            _context.Reservas.Remove(reserva);
        }

        _context.SaveChanges();

        TempData["Exito"] = "Préstamo registrado correctamente.";

        return RedirectToPage();
    }

    public IActionResult OnPostDevolver(int NumPrestamo)
    {
        var prestamo = _context.Prestamos
            .Include(p => p.FolioLibroNavigation)
            .FirstOrDefault(p => p.NumPrestamo == NumPrestamo);

        if (prestamo == null)
        {
            TempData["Error"] = "Préstamo no encontrado";
            return RedirectToPage();
        }

        if (prestamo.EstatusPrestamo == "Devuelto")
        {
            TempData["Error"] = "Este préstamo ya fue devuelto";
            return RedirectToPage();
        }

        prestamo.EstatusPrestamo = "Devuelto";

        prestamo.FolioLibroNavigation.NumeroCopias++;

        _context.SaveChanges();

        TempData["Exito"] = "Libro devuelto correctamente";
        return RedirectToPage();
    }

    public IActionResult OnPostAprobar(int NumPrestamo)
    {
        var prestamo = _context.Prestamos
            .FirstOrDefault(p => p.NumPrestamo == NumPrestamo);

        if (prestamo == null)
        {
            return NotFound();
        }

        prestamo.EstatusPrestamo = "Prestado";

        _context.SaveChanges();

        TempData["Exito"] = "Préstamo aprobado correctamente.";

        return RedirectToPage();
    }
}