using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class CopiaLibro
{
    public int IdCopia { get; set; }

    public string EstadoCopia { get; set; } = null!;

    public int FolioLibro { get; set; }

    public virtual Libro FolioLibroNavigation { get; set; } = null!;
}
