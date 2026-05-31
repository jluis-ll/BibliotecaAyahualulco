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
}