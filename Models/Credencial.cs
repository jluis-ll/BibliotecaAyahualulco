using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Credencial
{
    public int MatriculaCredencial { get; set; }

    public int Numero { get; set; }

    public virtual ICollection<Socio> Socios { get; set; } = new List<Socio>();
}
