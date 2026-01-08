using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class CrearDireccionDTO
    {
        public int IdUsuario { get; set; }
        public string TituloDireccion { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public string Telefono1 { get; set; } = string.Empty;
        public string? Telefono2 { get; set; }
        public decimal LatY { get; set; }
        public decimal LongX { get; set; }
    }

}
