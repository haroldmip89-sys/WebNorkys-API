using System;
using System.Collections.Generic;

namespace NorkysAPI.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
