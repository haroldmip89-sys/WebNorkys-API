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
    public class UsuarioDAO: IUsuarioDAO
    {
        private readonly MyDbContext _context;

        public UsuarioDAO(MyDbContext context)
        {
            _context = context;
        }

        // 1️ Validar login (email + password)
        public async Task<Usuario?> ValidateLogin(string email, string password)
        {
            // Aquí puedes agregar hashing si guardas contraseñas seguras
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }

        // 2️ Agregar un nuevo usuario
        public async Task<Usuario> Add(Usuario usuario)
        {
            // Verificar si ya existe un usuario con el mismo correo
            var existe = await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email);
            if (existe)
                return null; // o lanzar excepción, según cómo manejes errores

            // Agregar el usuario
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        // 3️ Eliminar usuario por Id
        public async Task<bool> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        // 4️ Verificar si un usuario es administrador
        public async Task<bool> IsAdmin(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            return usuario.EsAdmin; // suponiendo que tienes un campo bool EsAdmin
        }
        //buscar usuario por id
        public async Task<Usuario?> GetById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
        //listar todos
        public async Task<List<Usuario>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }
        public async Task<bool> UpdateUser(Usuario usuario)
        {
            var existente = await _context.Usuarios.FindAsync(usuario.IdUsuario);

            if (existente == null)
                return false;

            // SOLO los campos permitidos
            existente.Nombre = usuario.Nombre;
            existente.Apellido = usuario.Apellido;
            existente.DNI = usuario.DNI;
            existente.Email = usuario.Email;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
