using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Pages.Admin;

public class SancionesModel : PageModel
{
    public IList<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    private readonly ApplicationDbContext _context;

    public SancionesModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Sancion> Sanciones { get; set; } = new List<Sancion>();

    public void OnGet()
    {
        Sanciones = _context.Sancions
            .Include(s => s.NumPrestamoNavigation)
                .ThenInclude(p => p.NumSocioNavigation)
            .Include(s => s.NumPrestamoNavigation)
                .ThenInclude(p => p.FolioLibroNavigation)
            .ToList();

        Prestamos = _context.Prestamos
            .Include(p => p.NumSocioNavigation)
            .Include(p => p.FolioLibroNavigation)
            .ToList();
    }

    public IActionResult OnPost(
    int NumPrestamo,
    string Descripcion,
    int MontoSancion,
    DateTime LimitePago)
    {
        var prestamo = _context.Prestamos
            .FirstOrDefault(p => p.NumPrestamo == NumPrestamo);

        if (prestamo == null)
        {
            return NotFound();
        }

        var sancion = new Sancion
        {
            NumPrestamo = NumPrestamo,
            Descripcion = Descripcion,
            MontoSancion = MontoSancion,
            LimitePago = LimitePago,
            IdBibliotecario = prestamo.IdBibliotecario ?? 1
        };

        _context.Sancions.Add(sancion);

        _context.SaveChanges();

        TempData["Exito"] = "Sanción registrada correctamente.";

        return RedirectToPage();
    }
}