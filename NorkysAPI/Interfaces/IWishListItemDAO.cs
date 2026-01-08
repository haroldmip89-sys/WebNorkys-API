using NorkysAPI.DTO;
using NorkysAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Interfaces
{
    public interface IWishListItemDAO
    {
        Task<List<WishListItemDTO>> GetAll(int IdUsuario);
        Task<bool> Delete(int IdWishListItem);
        Task<bool> Add(WishListItem item);
    }
}
