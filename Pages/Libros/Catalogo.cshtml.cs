using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Pages.Libros;

public class CatalogoModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CatalogoModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Libro> Libros { get; set; } = new List<Libro>();

    public void OnGet()
    {
        Libros = _context.Libros.ToList();
    }
}