using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Telefono
{
    public int IdTelefono { get; set; }

    public string Numero { get; set; } = null!;

    public int NumSocio { get; set; }

    public virtual Socio NumSocioNavigation { get; set; } = null!;
}
