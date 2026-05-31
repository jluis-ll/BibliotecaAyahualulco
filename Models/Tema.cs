using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Tema
{
    public int IdTema { get; set; }

    public string NombTema { get; set; } = null!;

    public virtual ICollection<Ubicacion> Ubicacions { get; set; } = new List<Ubicacion>();
}
