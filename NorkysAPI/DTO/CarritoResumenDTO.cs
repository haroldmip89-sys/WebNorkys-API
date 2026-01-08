using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class CarritoResumenDTO
    {
        public int IdCarrito { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string MetodoPago { get; set; } = "";
    }
}
