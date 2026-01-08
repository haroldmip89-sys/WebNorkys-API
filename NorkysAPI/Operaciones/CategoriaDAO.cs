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
    public class CategoriaDAO:ICategoriaDAO
    {
        private readonly MyDbContext _context;

        public CategoriaDAO(MyDbContext context)
        {
            _context = context;
        }
        public async Task<List<Categoria>> GetAll()
        {
            return await _context.Categorias.ToListAsync();
        }
        public async Task<bool> UpdateNombre(int idCategoria, string nombre)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.IdCategoria == idCategoria);

            if (categoria == null)
                return false;

            categoria.Nombre = nombre;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Categoria?> GetById(int idCategoria)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.IdCategoria == idCategoria);
        }
                
        public async Task<bool> Delete(int idCategoria)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.IdCategoria == idCategoria);

            if (categoria == null)
                return false;

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
