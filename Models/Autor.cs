using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Libro> FolioLibros { get; set; } = new List<Libro>();
}
