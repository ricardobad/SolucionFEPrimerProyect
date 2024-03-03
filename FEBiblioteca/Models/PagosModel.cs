using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEBiblioteca.Models
{
    public class PagosModel
    {
        #region Propiedades
        public int Id_pago { get; set; }
        public int Id_reserva { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha_pago { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public decimal Descuento { get; set; }
        public decimal Multa { get; set; }
        public decimal Total { get; set; }
        #endregion

        #region Constructor

        public PagosModel()
        {
            Id_pago = 0;
            Id_reserva = 0;
            Monto = 0;
            Fecha_pago = DateTime.Now;
            Fecha_Fin = DateTime.Now;
            Descuento = 0;
            Multa = 0;
            Total = 0;

        }

     

        #endregion
    }
}
