using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Sancion
{
    public int Folio { get; set; }

    public DateTime LimitePago { get; set; }

    public string Descripcion { get; set; } = null!;

    public int MontoSancion { get; set; }

    public int NumPrestamo { get; set; }

    public int IdBibliotecario { get; set; }

    public virtual Prestamo NumPrestamoNavigation { get; set; } = null!;
}
