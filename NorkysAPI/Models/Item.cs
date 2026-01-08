using System;
using System.Collections.Generic;

namespace NorkysAPI.Models;

public partial class Item
{
    public int IdItem { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public string? ImagenUrl { get; set; }

    public int IdCategoria { get; set; }

    public virtual ICollection<CarritoDetalle> CarritoDetalles { get; set; } = new List<CarritoDetalle>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}
