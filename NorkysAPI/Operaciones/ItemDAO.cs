using Microsoft.EntityFrameworkCore;
using NorkysAPI.Data;
using NorkysAPI.Interfaces;
using NorkysAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Operaciones
{
    public class ItemDAO : IItemDAO
    {
        private readonly MyDbContext _context;
        public ItemDAO(MyDbContext context)
        {
            _context = context;
        }
        public async Task<Item> Add(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Item>> GetAll()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item?> GetById(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<Item?> Update(Item item)
        {
            var existingItem = await _context.Items.FindAsync(item.IdItem);
            if (existingItem == null) return null;

            existingItem.Nombre = item.Nombre;
            existingItem.Descripcion = item.Descripcion;
            existingItem.Precio = item.Precio;
            existingItem.ImagenUrl = item.ImagenUrl;
            //existingItem.IdCategoria = item.IdCategoria; //categoria no se debe actualizar

            await _context.SaveChangesAsync();
            return existingItem;
        }
        public async Task<List<Item>> GetByCategory(int idCategoria)
        {
            return await _context.Items.Where(i => i.IdCategoria == idCategoria).ToListAsync();
        }
    }
}
