using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    namespace NorkysWebAPI.DTOs
    {
        public class UsuarioUpdateDto
        {
            public string Nombre { get; set; } = null!;
            public string? Apellido { get; set; }
            public string? DNI { get; set; }
            public string Email { get; set; } = null!;
        }
    }

}
