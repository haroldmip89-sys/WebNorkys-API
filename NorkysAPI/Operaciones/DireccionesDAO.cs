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
    public class DireccionesDAO : IDireccionesDAO
    {
        private readonly MyDbContext _context;

        public DireccionesDAO(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Direcciones>> GetAll(int IdUsuario)
        {
            return await _context.Direcciones
                .Where(d => d.IdUsuario == IdUsuario)
                .ToListAsync();
        }

        public async Task<Direcciones> GetById(int IdDireccion)
        {
            return await _context.Direcciones.FindAsync(IdDireccion);
        }

        public async Task<Direcciones> Create(Direcciones direccion)
        {
            _context.Direcciones.Add(direccion);
            await _context.SaveChangesAsync();
            return direccion;
        }

        public async Task<bool> Update(int IdDireccion,Direcciones direccion)
        {
            var existe = await _context.Direcciones.AnyAsync(d => d.IdDireccion == IdDireccion);
            if (!existe) return false;
            direccion.IdDireccion = IdDireccion;
            _context.Direcciones.Update(direccion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int IdDireccion)
        {
            var entity = await _context.Direcciones.FindAsync(IdDireccion);
            if (entity == null) return false;

            _context.Direcciones.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
