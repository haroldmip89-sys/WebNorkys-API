using Microsoft.EntityFrameworkCore;
using NorkysAPI.Data;
using NorkysAPI.DTO;
using NorkysAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Operaciones
{
    public class DashboardDAO : IDashboardDAO
    {
        private readonly MyDbContext _context;

        public DashboardDAO(MyDbContext context)
        {
            _context = context;
        }

        // 1️⃣ Total de ventas por día
        public async Task<List<VentasPorDiaDTO>> ObtenerVentasPorDiaAsync(int topDias)
        {
            return await _context.Carritos
                .GroupBy(c => c.FechaCreacion.Date)
                .Select(g => new VentasPorDiaDTO
                {
                    Dia = g.Key,
                    TotalVentas = g.Sum(x => x.Total)
                })
                .OrderByDescending(x => x.Dia)
                .Take(topDias)
                .ToListAsync();
        }

        // 2️ Métodos de pago
        public async Task<List<MetodoPagoDTO>> ObtenerMetodosPagoAsync()
        {
            return await _context.Carritos
                .GroupBy(c => c.MetodoPago)
                .Select(g => new MetodoPagoDTO
                {
                    MetodoPago = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();
        }

        // 3️ PRODUCTOS MÁS VENDIDOS (CANTIDAD) – últimos N días
        public async Task<List<ProductoTopCantidadDTO>> ObtenerTopProductosVendidosAsync(int dias, int top)
        {
            DateTime fechaInicio = DateTime.Today.AddDays(-dias);

            return await _context.CarritoDetalles
                .Where(cd => cd.IdCarritoNavigation.FechaCreacion >= fechaInicio)
                .GroupBy(cd => new
                {
                    cd.IdItemNavigation.IdItem,
                    cd.IdItemNavigation.Nombre
                })
                .Select(g => new ProductoTopCantidadDTO
                {
                    Nombre = g.Key.Nombre,
                    TotalVendido = g.Sum(x => x.Cantidad)
                })
                .OrderByDescending(x => x.TotalVendido)
                .Take(top)
                .ToListAsync();
        }

        // 4 PRODCUTOS CON MAS GANANCIAS (SUMA DE TOTAL) #N DIAS
        public async Task<List<ProductoTopGananciaDTO>> ObtenerTopProductosGananciaAsync(int dias, int top)
        {
            DateTime fechaInicio = DateTime.Today.AddDays(-dias);

            return await _context.CarritoDetalles
                .Where(cd => cd.IdCarritoNavigation.FechaCreacion >= fechaInicio)
                .GroupBy(cd => new { cd.IdItemNavigation.IdItem, cd.IdItemNavigation.Nombre })
                .Select(g => new ProductoTopGananciaDTO
                {
                    Nombre = g.Key.Nombre,
                    Ganancia = g.Sum(x => x.Cantidad * x.PrecioUnitario)
                })
                .OrderByDescending(x => x.Ganancia)
                .Take(top)
                .ToListAsync();
        }

        // 5 
        public async Task<List<ProductoTopGananciaDTO>> ObtenerTopProductosGananciaPorFechaAsync(DateTime fecha)
        {
            return await _context.CarritoDetalles
                .Where(cd => cd.IdCarritoNavigation.FechaCreacion.Date == fecha.Date)
                .GroupBy(cd => new { cd.IdItemNavigation.IdItem, cd.IdItemNavigation.Nombre })
                .Select(g => new ProductoTopGananciaDTO
                {
                    Nombre = g.Key.Nombre,
                    Ganancia = g.Sum(x => x.Cantidad * x.PrecioUnitario)
                })
                .OrderByDescending(x => x.Ganancia)
                .Take(5)
                .ToListAsync();
        }

        // 6️ Fechas disponibles
        public async Task<List<FechaDTO>> ObtenerFechasDisponiblesAsync(int top)
        {
            return await _context.Carritos
                .GroupBy(c => c.FechaCreacion.Date)
                .Select(g => new FechaDTO
                {
                    Fecha = g.Key
                })
                .OrderByDescending(x => x.Fecha)
                .Take(top)
                .ToListAsync();
        }

        //7 Indicadores kpi
        public async Task<DashboardKpiDTO> ObtenerKpisAsync(int dias)
        {
            DateTime hoy = DateTime.Today;
            DateTime fechaInicio = hoy.AddDays(-dias);

            var query = _context.Carritos.AsQueryable();

            return new DashboardKpiDTO
            {
                // 1️⃣ Ventas pagadas últimos N días
                VentasPagadasUltimosDias = await query
                    .Where(c => c.Estado == "Pagado" &&
                                c.FechaCreacion >= fechaInicio)
                    .SumAsync(c => (decimal?)c.Total) ?? 0,

                // 2️⃣ Ventas pagadas HOY
                VentasPagadasHoy = await query
                    .Where(c => c.Estado == "Pagado" &&
                                c.FechaCreacion.Date == hoy)
                    .SumAsync(c => (decimal?)c.Total) ?? 0,

                // 3️⃣ Total pendiente por cobrar (TODOS)
                TotalPendienteCobro = await query
                    .Where(c => c.Estado == "Pendiente")
                    .SumAsync(c => (decimal?)c.Total) ?? 0,

                // 4️⃣ Cantidad carritos pagados
                CarritosPagadosUltimosDias = await query
                    .CountAsync(c => c.Estado == "Pagado" &&
                                     c.FechaCreacion >= fechaInicio),

                // 5️⃣ Cantidad carritos pendientes
                CarritosPendientesUltimosDias = await query
                    .CountAsync(c => c.Estado == "Pendiente" &&
                                     c.FechaCreacion >= fechaInicio),

                // 6️⃣ Cantidad carritos cancelados
                CarritosCanceladosUltimosDias = await query
                    .CountAsync(c => c.Estado == "Cancelado" &&
                                     c.FechaCreacion >= fechaInicio),

                // 7️⃣ Ticket promedio últimos N días
                TicketPromedioUltimosDias = await query
                    .Where(c => c.Estado == "Pagado" &&
                                c.FechaCreacion >= fechaInicio)
                    .AverageAsync(c => (decimal?)c.Total) ?? 0,

                // 8️⃣ Pedidos de HOY (todos los estados)
                PedidosHoy = await query
                    .CountAsync(c => c.FechaCreacion.Date == hoy)
            };
        }

    }
}
