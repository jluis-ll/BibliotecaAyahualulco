using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Pages.Admin;

public class DevolucionesModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DevolucionesModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public void OnGet()
    {
        Prestamos = _context.Prestamos
            .Include(p => p.FolioLibroNavigation)
            .Include(p => p.NumSocioNavigation)
            .Where(p => p.EstatusPrestamo != "Entregado")
            .ToList();
    }

    public IActionResult OnPost(int NumPrestamo)
    {
        var prestamo = _context.Prestamos
            .Include(p => p.FolioLibroNavigation)
            .FirstOrDefault(p => p.NumPrestamo == NumPrestamo);

        if (prestamo == null)
        {
            return NotFound();
        }

        prestamo.EstatusPrestamo = "Entregado";

        prestamo.FolioLibroNavigation.NumeroCopias++;

        _context.SaveChanges();

        TempData["Exito"] = "Devolución registrada correctamente.";

        return RedirectToPage();
    }
}