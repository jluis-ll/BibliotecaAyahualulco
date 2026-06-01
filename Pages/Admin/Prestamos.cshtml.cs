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
    DateTime FechaEntrega,
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
            TempData["Error"] = "No hay copias disponibles de este libro.";
            return RedirectToPage();
        }

        var prestamo = new Prestamo
        {
            NumSocio = NumSocio,
            FolioLibro = FolioLibro,
            FechaInicio = DateTime.Now,
            FechaEntrega = FechaEntrega,
            EstatusPrestamo = "Prestado",
            IdBibliotecario = IdBibliotecario
        };

        _context.Prestamos.Add(prestamo);

        libro.NumeroCopias--;

        _context.SaveChanges();

        TempData["Exito"] = "Préstamo registrado correctamente.";

        return RedirectToPage();
    }
}
