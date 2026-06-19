using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto.Data;
using Proyecto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Pages.Libros;

public class CatalogoModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Buscar { get; set; } = string.Empty;
    private readonly ApplicationDbContext _context;

    public CatalogoModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Libro> Libros { get; set; } = new List<Libro>();

    public void OnGet()
    {
        var consulta = _context.Libros.AsQueryable();

        if (!string.IsNullOrWhiteSpace(Buscar))
        {
            consulta = consulta.Where(l =>
                l.Nombre.Contains(Buscar) ||
                l.PaisPublicacion.Contains(Buscar) ||
                l.Isbn.ToString().Contains(Buscar) ||
                l.IdAutors.Any(a => a.Nombre.Contains(Buscar))
            );
        }

        Libros = consulta.ToList();
    }
}