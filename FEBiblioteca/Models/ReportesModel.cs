using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEBiblioteca.Models
{
    public class ReportesModel
    {
        #region Propiedades
        public string id_reporte { get; set; }
        public string Reporte { get; set; }


        #endregion

        #region Constructor

        public ReportesModel()
        {
            id_reporte = string.Empty;
            Reporte = string.Empty;
           
        }

        #endregion
    }
}
