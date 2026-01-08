using Microsoft.EntityFrameworkCore;
using NorkysAPI.Data;
using NorkysAPI.DTO;
using NorkysAPI.Interfaces;
using NorkysAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Operaciones
{
    public class WishListItemDAO : IWishListItemDAO
    {
        private readonly MyDbContext _context;

        public WishListItemDAO(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<WishListItemDTO>> GetAll(int IdUsuario)
        {
            return await _context.WishListItems
                .Where(w => w.IdUsuario == IdUsuario)
                .Include(w => w.Item)
                .Select(w => new WishListItemDTO
                {
                    IdWishList = w.IdWishListItem,
                    IdItem = w.Item.IdItem,
                    NombreItem = w.Item.Nombre,
                    Precio = w.Item.Precio,
                    ImagenUrl = w.Item.ImagenUrl
                })
                .ToListAsync();
        }


        public async Task<bool> Delete(int IdWishListItem)
        {
            var entity = await _context.WishListItems.FindAsync(IdWishListItem);
            if (entity == null) return false;

            _context.WishListItems.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Add(WishListItem item)
        {
            // verifica el unique constraint antes del insert
            var exists = await _context.WishListItems
                .AnyAsync(x => x.IdUsuario == item.IdUsuario && x.IdItem == item.IdItem);

            if (exists)
                return false;

            item.FechaAgregado = DateTime.Now;
            _context.WishListItems.Add(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<WishListItem?> GetById(int id)
        {
            return await _context.WishListItems.FindAsync(id);
        }
    }
}
