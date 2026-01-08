using NorkysAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Interfaces
{
    public interface IDireccionesDAO
    {
        Task<List<Direcciones>> GetAll(int IdUsuario);
        Task<Direcciones> GetById(int IdDireccion);
        Task<Direcciones> Create(Direcciones direccion);
        Task<bool> Update(int IdDireccion, Direcciones direccion);
        Task<bool> Delete(int IdDireccion);
    }
}
