using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Libro
{
    public int FolioLibro { get; set; }

    public string Nombre { get; set; } = null!;

    public int Isbn { get; set; }

    public string CondicionLibro { get; set; } = null!;

    public int NumeroPaginas { get; set; }

    public string PaisPublicacion { get; set; } = null!;

    public int NumeroCopias { get; set; }

    public int IdEditorial { get; set; }

    public int IdUbicacion { get; set; }

    public virtual ICollection<CopiaLibro> CopiaLibros { get; set; } = new List<CopiaLibro>();

    public virtual Editorial IdEditorialNavigation { get; set; } = null!;

    public virtual Ubicacion IdUbicacionNavigation { get; set; } = null!;

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Autor> IdAutors { get; set; } = new List<Autor>();
}
