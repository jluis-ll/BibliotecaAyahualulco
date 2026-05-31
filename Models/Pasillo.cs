using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Pasillo
{
    public int IdPasillo { get; set; }

    public string NomPasillo { get; set; } = null!;

    public virtual ICollection<Ubicacion> Ubicacions { get; set; } = new List<Ubicacion>();
}
