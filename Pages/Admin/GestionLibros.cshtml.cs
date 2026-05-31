using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Pages.Admin;

public class GestionLibrosModel : PageModel
{

    public IList<Autor> Autores { get; set; } = new List<Autor>();
    public IList<Editorial> Editoriales { get; set; } = new List<Editorial>();
    public IList<Tema> Temas { get; set; } = new List<Tema>();
    public IList<Pasillo> Pasillos { get; set; } = new List<Pasillo>();
    private readonly ApplicationDbContext _context;

    public GestionLibrosModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Libro> Libros { get; set; } = new List<Libro>();

    public void OnGet()
    {
        Libros = _context.Libros
            .Include(l => l.IdEditorialNavigation)
            .Include(l => l.IdAutors)
            .Include(l => l.IdUbicacionNavigation)
            .ToList();

        Autores = _context.Autors.ToList();
        Editoriales = _context.Editorials.ToList();
        Temas = _context.Temas.ToList();
        Pasillos = _context.Pasillos.ToList();
    }

    public IActionResult OnPost(
    string Nombre,
    int Isbn,
    string CondicionLibro,
    int NumeroPaginas,
    string PaisPublicacion,
    int NumeroCopias,
    int? IdAutor,
    string? NuevoAutor,
    int? IdEditorial,
    string? NuevaEditorial,
    int? IdTema,
    string? NuevoTema,
    int IdPasillo,
    string Piso)
    {
        // ===== AUTOR =====

        Autor autor;

        if (!string.IsNullOrWhiteSpace(NuevoAutor))
        {
            autor = new Autor
            {
                Nombre = NuevoAutor
            };

            _context.Autors.Add(autor);
            _context.SaveChanges();
        }
        else
        {
            autor = _context.Autors
                .First(a => a.IdAutor == IdAutor);
        }

        // ===== EDITORIAL =====

        Editorial editorial;

        if (!string.IsNullOrWhiteSpace(NuevaEditorial))
        {
            editorial = new Editorial
            {
                Nombre = NuevaEditorial
            };

            _context.Editorials.Add(editorial);
            _context.SaveChanges();
        }
        else
        {
            editorial = _context.Editorials
                .First(e => e.IdEditorial == IdEditorial);
        }

        // ===== TEMA =====

        Tema tema;

        if (!string.IsNullOrWhiteSpace(NuevoTema))
        {
            tema = new Tema
            {
                NombTema = NuevoTema
            };

            _context.Temas.Add(tema);
            _context.SaveChanges();
        }
        else
        {
            tema = _context.Temas
                .First(t => t.IdTema == IdTema);
        }

        // ===== UBICACION =====

        var ubicacion = new Ubicacion
        {
            Piso = Piso,
            IdTema = tema.IdTema,
            IdPasillo = IdPasillo
        };

        _context.Ubicacions.Add(ubicacion);
        _context.SaveChanges();

        // ===== LIBRO =====

        var libro = new Libro
        {
            Nombre = Nombre,
            Isbn = Isbn,
            CondicionLibro = CondicionLibro,
            NumeroPaginas = NumeroPaginas,
            PaisPublicacion = PaisPublicacion,
            NumeroCopias = NumeroCopias,
            IdEditorial = editorial.IdEditorial,
            IdUbicacion = ubicacion.IdUbicacion
        };

        libro.IdAutors.Add(autor);

        _context.Libros.Add(libro);

        _context.SaveChanges();

        return RedirectToPage();
    }

    public IActionResult OnPostEditar(
    int FolioLibro,
    int NumeroCopias,
    string CondicionLibro,
    int IdPasillo,
    string Piso)
    {
        var libro = _context.Libros
            .Include(l => l.IdUbicacionNavigation)
            .FirstOrDefault(l => l.FolioLibro == FolioLibro);

        if (libro == null)
        {
            return NotFound();
        }

        // ===== ACTUALIZAR LIBRO =====

        libro.NumeroCopias = NumeroCopias;
        libro.CondicionLibro = CondicionLibro;

        // ===== ACTUALIZAR UBICACION =====

        if (libro.IdUbicacionNavigation != null)
        {
            libro.IdUbicacionNavigation.IdPasillo = IdPasillo;
            libro.IdUbicacionNavigation.Piso = Piso;
        }

        _context.SaveChanges();

        return RedirectToPage();
    }

}