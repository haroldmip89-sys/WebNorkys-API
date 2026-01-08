using System;
using System.Collections.Generic;

namespace NorkysAPI.Models;

public partial class CarritoDetalle
{
    public int IdCarritoDetalle { get; set; }

    public int IdCarrito { get; set; }

    public int IdItem { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Carrito? IdCarritoNavigation { get; set; } = null!;

    public virtual Item IdItemNavigation { get; set; } = null!;
}
