using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorkysAPI.DTO
{
    public class VentasPorDiaDTO
    {
        public DateTime Dia { get; set; }
        public decimal TotalVentas { get; set; }
    }

    public class MetodoPagoDTO
    {
        public string MetodoPago { get; set; }
        public int Cantidad { get; set; }
    }

    public class ProductoTopCantidadDTO
    {
        public string Nombre { get; set; }
        public int TotalVendido { get; set; }
    }

    public class ProductoTopGananciaDTO
    {
        public string Nombre { get; set; }
        public decimal Ganancia { get; set; }
    }

    public class FechaDTO
    {
        public DateTime Fecha { get; set; }
    }

    public class DashboardKpiDTO
    {
        public decimal VentasPagadasUltimosDias { get; set; }
        public decimal VentasPagadasHoy { get; set; }

        public decimal TotalPendienteCobro { get; set; }

        public int CarritosPagadosUltimosDias { get; set; }
        public int CarritosPendientesUltimosDias { get; set; }
        public int CarritosCanceladosUltimosDias { get; set; }

        public decimal TicketPromedioUltimosDias { get; set; }

        public int PedidosHoy { get; set; }
    }

}
