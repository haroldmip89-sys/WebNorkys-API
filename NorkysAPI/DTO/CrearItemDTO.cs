using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class CrearItemDTO
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        //Imagen(antes string ImagenUrl )
        public IFormFile? Imagen { get; set; }
        public int IdCategoria { get; set; }
    }
}
