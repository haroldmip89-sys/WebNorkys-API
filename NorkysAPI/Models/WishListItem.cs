using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Models
{
    public class WishListItem
    {
        public int IdWishListItem { get; set; }
        public int IdUsuario { get; set; }
        public int IdItem { get; set; }
        public DateTime FechaAgregado { get; set; }

        // Relaciones (opcional pero recomendado)
        public Usuario Usuario { get; set; }
        public Item Item { get; set; }
    }
}
