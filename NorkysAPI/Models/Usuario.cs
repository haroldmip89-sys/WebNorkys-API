using System;
using System.Collections.Generic;

namespace NorkysAPI.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool EsAdmin { get; set; }
    // Nuevos campos
    public string? Apellido { get; set; }
    public string? DNI { get; set; }


    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();
    public virtual ICollection<Direcciones> Direcciones { get; set; } = new List<Direcciones>(); // <-- Agregar esta propiedad
}
