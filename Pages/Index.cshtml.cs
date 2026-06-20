using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<LibroPopularViewModel> LibrosPopulares { get; set; } = new List<LibroPopularViewModel>();

    public void OnGet()
    {
        LibrosPopulares = _context.Libros
            .Select(l => new LibroPopularViewModel
            {
                Libro = l,
                TotalMovimientos =
                    _context.Prestamos.Count(p => p.FolioLibro == l.FolioLibro) +
                    _context.Reservas.Count(r => r.FolioLibro == l.FolioLibro)
            })
            .OrderByDescending(l => l.TotalMovimientos)
            .Take(3)
            .ToList();
    }
}

public class LibroPopularViewModel
{
    public Libro Libro { get; set; } = null!;
    public int TotalMovimientos { get; set; }
}