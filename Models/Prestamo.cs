using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Prestamo
{
    public int NumPrestamo { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaEntrega { get; set; }

    public string EstatusPrestamo { get; set; } = null!;

    public int FolioLibro { get; set; }

    public int NumSocio { get; set; }

    public int? IdBibliotecario { get; set; }

    public virtual Libro FolioLibroNavigation { get; set; } = null!;

    public virtual Bibliotecario? IdBibliotecarioNavigation { get; set; }

    public virtual Socio NumSocioNavigation { get; set; } = null!;

    public virtual ICollection<Sancion> Sancions { get; set; } = new List<Sancion>();
}
