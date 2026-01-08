using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Models
{
    public class Direcciones
    {
        public int IdDireccion { get; set; }
        public int IdUsuario { get; set; }
        public string TituloDireccion { get; set; } = "";
        public string Referencia { get; set; }
        public string? Direccion { get; set; }
        public string Telefono1 { get; set; } = "";
        public string? Telefono2 { get; set; }
        public decimal? LatY { get; set; }
        public decimal? LongX { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
    }
}
