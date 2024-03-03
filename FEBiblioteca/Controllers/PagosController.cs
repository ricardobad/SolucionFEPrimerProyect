using FEBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEBiblioteca.Controllers
{
    public class PagosController : Controller
    {

        public async Task<IActionResult> Index()
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<PagosModel> lstresultados = await objconexion.ListarPagos();
            return View(lstresultados);
        }

        public IActionResult CrearPago()
        {
            return View();
        }


        public async Task<IActionResult> EliminarPagos(int pPagos)
        {
            ReportesModel aux = new ReportesModel();
            aux.Reporte = "Se eliminó el pago " + pPagos + " con fecha " + DateTime.Now;
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.EliminarPagos(new PagosModel { Id_pago = pPagos });
            await objconexion.AgregarReporte(aux);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> VerInfoModificar(int pPago)
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<PagosModel> lstresultados = await objconexion.ConsultarPago(new PagosModel { Id_pago = pPago });
            return View(lstresultados.FirstOrDefault());
        }


        public IActionResult Ventas()
        {
            return View();
        }

        public async Task<IActionResult> VerMontoTotal(PagosModel P_Pago)
        {
           
            GestorConexiones objconexion = new GestorConexiones();

            // Llama al método ConsultarTotalGenerado para obtener el total generado en el rango de fechas
            List<PagosModel> lstresultados = await objconexion.ConsultarMontoTotal(P_Pago);

            foreach (PagosModel pago in lstresultados)
            {
                decimal totalGenerado;
                totalGenerado = pago.Total;
                ViewData["TotalGenerado"] = totalGenerado;
                // Aquí puedes hacer lo que necesites con el totalGenerado, por ejemplo, pasarlo a la vista o usarlo en otra parte del código
                // En este ejemplo, lo estamos almacenando en ViewData para pasarlo a la vista
            }



            return View(lstresultados);
        }
        public async Task<IActionResult> ModificarPagos(PagosModel P_Pagos)
        {
            ReportesModel aux = new ReportesModel();
            aux.Reporte = "Se modificó el pago " + P_Pagos.Id_pago + " con fecha " + DateTime.Now;
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.ModificarPagos(P_Pagos);
            await objconexion.AgregarReporte(aux);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPago(PagosModel P_Pagos)
        {

            ErrorViewModel msj = new ErrorViewModel();
            GestorConexiones objconexion = new GestorConexiones();
            List<PagosModel> lstresultados = await objconexion.ConsultarPago(new PagosModel { Id_pago = P_Pagos.Id_pago });
            lstresultados.FirstOrDefault();
            if (lstresultados.Count == 0)
            {
                ReportesModel aux = new ReportesModel();
                aux.Reporte = "Se agregó el pago " + P_Pagos.Id_pago + " con fecha " + DateTime.Now;
                await objconexion.AgregarPagos(P_Pagos);
                await objconexion.AgregarReporte(aux);
                return RedirectToAction("Index");
            }
            else
            {
                msj.RequestId = "El usuario tiene una reserva por lo que no se puede eliminar";
                return RedirectToAction("Index");
            }
        }

       
      
    }
}
