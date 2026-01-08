using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class CarritoDetalleDTO
    {
        public int IdCarritoDetalle { get; set; }
        //public int IdCarrito { get; set; } // opcional pero recomendable
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        public int IdItem { get; set; }
        public string NombreItem { get; set; }
        public string ImagenUrl { get; set; }
    }
}
