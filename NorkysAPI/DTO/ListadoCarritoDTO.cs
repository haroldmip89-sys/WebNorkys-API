using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class ListadoCarritoDTO
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

        // Guest checkout
        public string? NombreCliente { get; set; }
        public string? ApellidoCliente { get; set; }
        public string? DNI { get; set; }
        public string? EmailCliente { get; set; }
    }
}
