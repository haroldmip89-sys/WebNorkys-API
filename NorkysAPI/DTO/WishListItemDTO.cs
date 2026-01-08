using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class WishListItemDTO
    {
        public int IdWishList { get; set; }
        public int IdItem { get; set; }
        public string NombreItem { get; set; }
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }
    }

}
