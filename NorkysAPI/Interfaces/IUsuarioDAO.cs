using NorkysAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Interfaces
{
    public interface IUsuarioDAO
    {
        // Validar login (email + password)
        Task<Usuario?> ValidateLogin(string email, string password);

        // Agregar un nuevo usuario
        Task<Usuario> Add(Usuario usuario);

        // Eliminar usuario por Id
        Task<bool> Delete(int id);

        // Verificar si un usuario es administrador
        Task<bool> IsAdmin(int id);

        //Obtener por ID
        Task<Usuario?> GetById(int id);

        //Listar todos
        Task<List<Usuario>> GetAll();
        //modificar 
        Task<bool> UpdateUser(Usuario usuario);
    }
}
