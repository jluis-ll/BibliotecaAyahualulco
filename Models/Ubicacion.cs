using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Ubicacion
{
    public int IdUbicacion { get; set; }

    public string Piso { get; set; } = null!;

    public int IdTema { get; set; }

    public int IdPasillo { get; set; }

    public virtual Pasillo IdPasilloNavigation { get; set; } = null!;

    public virtual Tema IdTemaNavigation { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
