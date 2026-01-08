using NorkysAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.Interfaces
{
    public interface IDashboardDAO
    {
        // 1️ Total de ventas por día (últimos N días)
        Task<List<VentasPorDiaDTO>> ObtenerVentasPorDiaAsync(int topDias);

        // 2️ Métodos de pago
        Task<List<MetodoPagoDTO>> ObtenerMetodosPagoAsync();

        // 3️ Top productos más vendidos (últimos N días)
        Task<List<ProductoTopCantidadDTO>> ObtenerTopProductosVendidosAsync(int dias, int top);

        // 4️ Top productos con mayor ganancia (últimos N días)
        Task<List<ProductoTopGananciaDTO>> ObtenerTopProductosGananciaAsync(int dias, int top);

        // 5️ Top productos con mayor ganancia por día específico
        Task<List<ProductoTopGananciaDTO>> ObtenerTopProductosGananciaPorFechaAsync(DateTime fecha);

        // 6️ Fechas disponibles para filtro
        Task<List<FechaDTO>> ObtenerFechasDisponiblesAsync(int top);

        Task<DashboardKpiDTO> ObtenerKpisAsync(int dias);
    }
}
