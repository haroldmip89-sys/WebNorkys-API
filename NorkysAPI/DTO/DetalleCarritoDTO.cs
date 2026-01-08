using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class DetalleCarritoDTO
    {
        //tabla para hacer el insert en el sp
        public int IdItem { get; set; }
        public int Cantidad { get; set; }
    }
}
