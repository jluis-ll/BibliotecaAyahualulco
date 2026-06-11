using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int NumSocio { get; set; }

    public int FolioLibro { get; set; }

    public DateTime FechaReserva { get; set; }

    public virtual Libro FolioLibroNavigation { get; set; } = null!;

    public virtual Socio NumSocioNavigation { get; set; } = null!;
}