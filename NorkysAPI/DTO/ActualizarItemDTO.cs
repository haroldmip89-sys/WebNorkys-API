using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NorkysAPI.DTO
{
    public class ActualizarItemDTO
    {
        //es lo mismo que el crear pero no permite modificar la categoria
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        //Imagen(antes string ImagenUrl )
        public IFormFile? Imagen { get; set; }

    }
}
