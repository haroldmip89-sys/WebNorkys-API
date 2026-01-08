using NorkysAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Interfaces
{
    public interface ICategoriaDAO
    {
        Task<List<Categoria?>> GetAll();
        Task<bool> UpdateNombre(int idCategoria, string nombre);
        Task<Categoria?> GetById(int idCategoria);

    }
}
