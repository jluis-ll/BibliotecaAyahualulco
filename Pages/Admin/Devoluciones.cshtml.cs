using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.Pages.Admin;

public class DevolucionesModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Buscar { get; set; } = string.Empty;
    private readonly ApplicationDbContext _context;

    public DevolucionesModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public void OnGet()
    {
        var consulta = _context.Prestamos
            .Include(p => p.FolioLibroNavigation)
            .Include(p => p.NumSocioNavigation)
            .Where(p => p.EstatusPrestamo == "Prestado")
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(Buscar))
        {
            consulta = consulta.Where(p =>
                p.NumPrestamo.ToString().Contains(Buscar) ||
                p.NumSocioNavigation.NombCompleto.Contains(Buscar) ||
                p.FolioLibroNavigation.Nombre.Contains(Buscar));
        }

        Prestamos = consulta.ToList();
    }

    public IActionResult OnPost(
    int NumPrestamo,
    bool AplicaSancion,
    string? Descripcion,
    int? MontoSancion,
    DateTime? LimitePago)
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

        if (AplicaSancion)
        {
            var sancion = new Sancion
            {
                NumPrestamo = NumPrestamo,
                Descripcion = Descripcion ?? "Sanción por devolución",
                MontoSancion = MontoSancion ?? 0,
                LimitePago = LimitePago ?? DateTime.Now.AddDays(7),
                IdBibliotecario = prestamo.IdBibliotecario ?? 1
            };

            _context.Sancions.Add(sancion);
        }

        _context.SaveChanges();

        TempData["Exito"] = "Devolución registrada correctamente.";

        return RedirectToPage();
    }
}