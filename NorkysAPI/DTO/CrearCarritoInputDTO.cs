using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class CrearCarritoInputDTO
    {
        public int? IdUsuario { get; set; }            // nullable

        // Datos invitado (opcionales si IdUsuario != null)
        public string? NombreCliente { get; set; }
        public string? ApellidoCliente { get; set; }
        public string? EmailCliente { get; set; }
        public string DNICliente { get; set; } = null!;

        // Datos de dirección
        public string TituloDireccion { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string? Referencia { get; set; }
        public string Telefono1 { get; set; } = null!;
        public string? Telefono2 { get; set; }

        public decimal? LatY { get; set; }
        public decimal? LongX { get; set; }
        public List<DetalleCarritoDTO> Detalles { get; set; } = new List<DetalleCarritoDTO>();
    }

}
