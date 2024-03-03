using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEBiblioteca.Models;

namespace FEBiblioteca.Controllers
{
    public class ReportesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<ReportesModel> lstresultados = await objconexion.ListaReportes();
            return View(lstresultados);
        }

        public IActionResult AperturaCrearReporte()
        {
            return View();
        }

        public async Task<IActionResult> VerInfoModificar(string pReporte)
        {
            GestorConexiones objconexion = new GestorConexiones();
            ReportesModel entidad = await objconexion.ConsultarReportePorID(new ReportesModel { id_reporte = pReporte });
            return View(entidad);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarReporte(ReportesModel P_Reporte)
        {
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.AgregarReporte(P_Reporte);
            return RedirectToAction("Index");
        }
      
    }
}
