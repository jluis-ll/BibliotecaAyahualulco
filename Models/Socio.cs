using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Socio
{
    public string Contrasena { get; set; } = null!;
    public int NumSocio { get; set; }

    public string NombCompleto { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public int MatriculaCredencial { get; set; }

    public virtual Credencial MatriculaCredencialNavigation { get; set; } = null!;

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}
