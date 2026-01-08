using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class EditarDireccionCarritoDTO
    {
        public string TituloDireccion { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public decimal? LatY { get; set; }
        public decimal? LongX { get; set; }
    }
}
