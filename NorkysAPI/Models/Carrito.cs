using System;
using System.Collections.Generic;

namespace NorkysAPI.Models;

public partial class Carrito
{
    public int IdCarrito { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime FechaCreacion { get; set; }

    public decimal Total { get; set; }

    public string Estado { get; set; } = null!;
    public string? MetodoPago { get; set; }

    public int? IdDireccion { get; set; }

    public string TituloDireccion { get; set; } = string.Empty;

    public string? Direccion { get; set; }

    public string? Referencia { get; set; }

    public string? Telefono1 { get; set; }

    public string? Telefono2 { get; set; }

    public decimal? LatY { get; set; }

    public decimal? LongX { get; set; }

    // Nuevos campos para guest checkout
    public string? NombreCliente { get; set; }
    public string? ApellidoCliente { get; set; }
    public string? DNI { get; set; }
    public string? EmailCliente { get; set; }

    public virtual ICollection<CarritoDetalle> CarritoDetalles { get; set; } = new List<CarritoDetalle>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
