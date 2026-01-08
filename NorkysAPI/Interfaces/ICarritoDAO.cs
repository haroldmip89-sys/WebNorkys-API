using NorkysAPI.DTO;
using NorkysAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Interfaces
{
    public interface ICarritoDAO
    {
        // LISTAR CARRITOS POR USUARIO
        Task<List<CarritoResumenDTO>> GetCarritosByUsuario(int usuarioId);

        // LISTAR DETALLES DE UN CARRITO
        Task<List<CarritoDetalleDTO>> GetCarritoDetalles(int carritoId);

        // CREAR CARRITO USANDO SP
        Task<int> CrearCarritoConSp(
            int? IdUsuario,
            string? NombreCliente,
            string? ApellidoCliente,
            string? EmailCliente,
            string? DNICliente,
            string TituloDireccion,
            string Direccion,
            string? Referencia,
            string Telefono1,
            string? Telefono2,
            decimal? LatY,
            decimal? LongX,
            List<DetalleCarritoDTO> detalles);

        // CAMBIAR ESTADO
        Task<bool> UpdateEstado(int carritoId, string estado);

        //PENDIENTE BORRAR CARRITO
        Task<bool> DeleteCarrito(int carritoId);

        //ACTUALIZAR METODO PAGO
        Task<bool> UpdateMetodoPago(int IdCarrito, string metodoPago);

        //seleccionar por id
        Task<Carrito?> GetById(int id);
        // AGREGAR ITEM MANUAL (si no usas SP)
        //Task<CarritoDetalleDTO> AddItem(int carritoId, int itemId, int cantidad);

        // SELECCIONAR DIRECCION PARA EL CARRITO DESDE DIRECCIONES GUARDADAS
        Task<bool> UpdateDireccion(int IdCarrito, int IdDireccion);

        //ACTUALIZAR DIRECCION ALMACENADA DEL CARRITO
        //Task<bool> ActualizarCarritoDireccion(Carrito carrito);
        //seleccionar todos los carritos por estado
        Task<List<ListadoCarritoDTO>> GetAllPorEstado(string estado);


    }
}
