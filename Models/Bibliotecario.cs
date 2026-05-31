using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Bibliotecario
{
    public int IdBibliotecario { get; set; }

    public string Nombre { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
