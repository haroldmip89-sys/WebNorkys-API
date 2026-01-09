using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorkysAPI.Data;
using NorkysAPI.DTO;
using NorkysAPI.Interfaces;
using NorkysAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace NorkysAPI.Operaciones
{
    public class CarritoDAO:ICarritoDAO
    {
        private readonly MyDbContext _context;

        public CarritoDAO(MyDbContext context)
        {
            _context = context;
        }

        // LISTAR CARRITOS POR USUARIO
        public async Task<List<CarritoResumenDTO>> GetCarritosByUsuario(int IdUsuario)
        {
            return await _context.Carritos
                .Where(c => c.IdUsuario == IdUsuario)
                .Select(c => new CarritoResumenDTO
                {
                    IdCarrito = c.IdCarrito,
                    FechaCreacion = c.FechaCreacion,
                    Total = c.Total,
                    Estado = c.Estado,
                    Direccion=c.Direccion,//solo direccion y metodo pago
                    MetodoPago =c.MetodoPago
                })
                .ToListAsync();
        }

        // LISTAR DETALLES DE UN CARRITO
        public async Task<List<CarritoDetalleDTO>> GetCarritoDetalles(int IdCarrito)
        {
            return await _context.CarritoDetalles
                .Where(d => d.IdCarrito == IdCarrito)
                .Select(d => new CarritoDetalleDTO
                {
                    IdCarritoDetalle = d.IdCarritoDetalle, // ← IMPORTANTE
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,

                    IdItem = d.IdItem,
                    NombreItem = d.IdItemNavigation.Nombre,
                    ImagenUrl = d.IdItemNavigation.ImagenUrl
                })
                .ToListAsync();
        }

        // CAMBIAR ESTADO
        public async Task<bool> UpdateEstado(int IdCarrito, string estado)
        {
            var carrito = await _context.Carritos.FindAsync(IdCarrito);

            if (carrito == null)
                return false;

            carrito.Estado = estado;

            await _context.SaveChangesAsync();
            return true;
        }

        // CAMBIAR MÉTODO DE PAGO
        public async Task<bool> UpdateMetodoPago(int IdCarrito, string metodoPago)
        {
            var carrito = await _context.Carritos.FindAsync(IdCarrito);

            if (carrito == null)
                return false;

            carrito.MetodoPago = metodoPago;

            await _context.SaveChangesAsync();
            return true;
        }

        //BORRAR CARRITO

        public async Task<bool> DeleteCarrito(int IdCarrito)
        {
            // Inicia transacción para mayor seguridad
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Buscar los detalles del carrito
                var detalles = await _context.CarritoDetalles
                    .Where(cd => cd.IdCarrito == IdCarrito)
                    .ToListAsync();

                if (detalles.Any())
                {
                    _context.CarritoDetalles.RemoveRange(detalles);
                    await _context.SaveChangesAsync();
                }

                // 2. Buscar el carrito
                var carrito = await _context.Carritos.FindAsync(IdCarrito);
                if (carrito == null) return false;

                // 3. Borrar el carrito
                _context.Carritos.Remove(carrito);
                await _context.SaveChangesAsync();

                // 4. Confirmar transacción
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<Carrito?> GetById(int IdCarrito)
        {
            return await _context.Carritos.FindAsync(IdCarrito);
        }

        // CREAR CARRITO USANDO SP
        public async Task<int> CrearCarritoConSp(
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
    List<DetalleCarritoDTO> detalles)
        {
            await using var conn = new NpgsqlConnection(
    "Host=dpg-d5fldc7pm1nc73dgfut0-a.virginia-postgres.render.com;" +
    "Port=5432;" +
    "Database=Norkys;" +
    "Username=norkys_user;" +
    "Password=Xev6PVWRexAmN7pwFlkRJTkLDG7d0UbA;" +
    "SSL Mode=Require;Trust Server Certificate=true"
);

            await conn.OpenAsync();

            var sql = @"
        SELECT CrearCarrito(
            @p_IdUsuario,
            @p_NombreCliente,
            @p_ApellidoCliente,
            @p_EmailCliente,
            @p_DNICliente,
            @p_TituloDireccion,
            @p_Direccion,
            @p_Referencia,
            @p_Telefono1,
            @p_Telefono2,
            @p_LatY,
            @p_LongX,
            @p_Detalles
        );
    ";

            await using var cmd = new NpgsqlCommand(sql, conn);

            // JSONB detalles
            var detallesJson = System.Text.Json.JsonSerializer.Serialize(
                detalles.Select(d => new
                {
                    IdItem = d.IdItem,
                    Cantidad = d.Cantidad
                })
            );

            cmd.Parameters.AddWithValue("p_IdUsuario", (object?)IdUsuario ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p_NombreCliente", (object?)NombreCliente ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p_ApellidoCliente", (object?)ApellidoCliente ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p_EmailCliente", (object?)EmailCliente ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p_DNICliente", (object?)DNICliente ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p_TituloDireccion", TituloDireccion);
            cmd.Parameters.AddWithValue("p_Direccion", Direccion);
            cmd.Parameters.AddWithValue("p_Referencia", (object?)Referencia ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p_Telefono1", Telefono1);
            cmd.Parameters.AddWithValue("p_Telefono2", (object?)Telefono2 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p_LatY", (object?)LatY ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p_LongX", (object?)LongX ?? DBNull.Value);
            cmd.Parameters.AddWithValue("p_Detalles", NpgsqlTypes.NpgsqlDbType.Jsonb, detallesJson);

            var result = await cmd.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }


        // ACTUALIZAR DIRECCIÓN DEL CARRITO
        public async Task<bool> UpdateDireccion(int IdCarrito, int IdDireccion)
        {
            var carrito = await _context.Carritos.FindAsync(IdCarrito);
            if (carrito == null)
                return false;

            var direccion = await _context.Direcciones.FindAsync(IdDireccion);
            if (direccion == null)
                return false;

            // VALIDACIÓN IMPORTANTE
            if (direccion.IdUsuario != carrito.IdUsuario)
                return false; // evita asignar direcciones de otros usuarios 

            // Asignamos los datos al carrito
            carrito.IdDireccion = direccion.IdDireccion;
            carrito.TituloDireccion = direccion.TituloDireccion;
            carrito.Direccion = direccion.Direccion;
            carrito.Referencia = direccion.Referencia;
            carrito.Telefono1 = direccion.Telefono1;
            carrito.Telefono2 = direccion.Telefono2;
            carrito.LatY = direccion.LatY;
            carrito.LongX = direccion.LongX;

            await _context.SaveChangesAsync();
            return true;
        }
        //public async Task<bool> ActualizarCarritoDireccion(Carrito carrito)
        //{
        //    using var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
        //    await conn.OpenAsync();

        //    var sql = @"
        //        UPDATE Carrito
        //        SET 
        //            TituloDireccion = @TituloDireccion,
        //            Direccion = @Direccion,
        //            Referencia = @Referencia,
        //            Telefono1 = @Telefono1,
        //            Telefono2 = @Telefono2,
        //            LatY = @LatY,
        //            LongX = @LongX
        //        WHERE IdCarrito = @IdCarrito";

        //    using var cmd = new SqlCommand(sql, conn);

        //    cmd.Parameters.AddWithValue("@TituloDireccion", carrito.TituloDireccion);
        //    cmd.Parameters.AddWithValue("@Direccion", carrito.Direccion ?? (object)DBNull.Value);
        //    cmd.Parameters.AddWithValue("@Referencia", carrito.Referencia ?? (object)DBNull.Value);
        //    cmd.Parameters.AddWithValue("@Telefono1", carrito.Telefono1 ?? (object)DBNull.Value);
        //    cmd.Parameters.AddWithValue("@Telefono2", carrito.Telefono2 ?? (object)DBNull.Value);
        //    cmd.Parameters.AddWithValue("@LatY", carrito.LatY.HasValue ? carrito.LatY : (object)DBNull.Value);
        //    cmd.Parameters.AddWithValue("@LongX", carrito.LongX.HasValue ? carrito.LongX : (object)DBNull.Value);
        //    cmd.Parameters.AddWithValue("@IdCarrito", carrito.IdCarrito);

        //    var rows = await cmd.ExecuteNonQueryAsync();
        //    return rows > 0;
        //}
        public async Task<List<ListadoCarritoDTO>> GetAllPorEstado(string estado)
        {
            return await _context.Carritos
                .Where(c => c.Estado == estado)
                .Select(c => new ListadoCarritoDTO
                {
                    IdCarrito = c.IdCarrito,
                    IdUsuario = c.IdUsuario,
                    FechaCreacion = c.FechaCreacion,
                    Total = c.Total,
                    Estado = c.Estado,
                    MetodoPago = c.MetodoPago,
                    IdDireccion = c.IdDireccion,
                    TituloDireccion = c.TituloDireccion,
                    Direccion = c.Direccion,
                    Referencia = c.Referencia,
                    Telefono1 = c.Telefono1,
                    Telefono2 = c.Telefono2,
                    LatY = c.LatY,
                    LongX = c.LongX,
                    NombreCliente = c.NombreCliente,
                    ApellidoCliente = c.ApellidoCliente,
                    DNI = c.DNI,
                    EmailCliente = c.EmailCliente
                })
                .ToListAsync();
        }
    }
}
