using FEBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEBiblioteca.Controllers
{
    public class HabitacionesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<HabitacionesModel> lstresultados = await objconexion.ListarHabitaciones();
            return View(lstresultados);
        }

        public IActionResult AperturaCrearUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarHabitaciones(HabitacionesModel P_Habitaciones)
        {

            ErrorViewModel msj = new ErrorViewModel();
            GestorConexiones objconexion = new GestorConexiones();
            List<HabitacionesModel> lstresultados = await objconexion.ConsultarHabitacion(new HabitacionesModel { Id_habitacion = P_Habitaciones.Id_habitacion });
            lstresultados.FirstOrDefault();
            if (lstresultados.Count == 0)
            {
                ReportesModel aux = new ReportesModel();
                aux.Reporte = "Se agregó la habitación " + P_Habitaciones.Id_habitacion + " con fecha " + DateTime.Now;
                await objconexion.AgregarHabitaciones(P_Habitaciones);
                await objconexion.AgregarReporte(aux);
                return RedirectToAction("Index");
            }
            else
            {
                msj.RequestId = "El usuario tiene una reserva por lo que no se puede eliminar";
                return RedirectToAction("Index");
            }
           
           
        }
      
        public async Task<IActionResult> EliminarHabitacion(int pHabitacion)
        {
            ReportesModel aux = new ReportesModel();
            aux.Reporte = "Se eliminó la habitación " + pHabitacion + " con fecha " + DateTime.Now;
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.EliminarHabitacion(new HabitacionesModel { Id_habitacion = pHabitacion });
            await objconexion.AgregarReporte(aux);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> VerInfoModificar(int pHabitacion)
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<HabitacionesModel> lstresultados = await objconexion.ConsultarHabitacion(new HabitacionesModel { Id_habitacion = pHabitacion });
            return View(lstresultados.FirstOrDefault());
        }
        public async Task<IActionResult> ModificarHabitacion(HabitacionesModel P_Habitacion)
        {
            ReportesModel aux = new ReportesModel();
            aux.Reporte = "Se modificó la habitación " + P_Habitacion.Id_habitacion + " con fecha " + DateTime.Now;
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.ModificarHabitacion(P_Habitacion);
            await objconexion.AgregarReporte(aux);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ConsultarHabitacionOcupada(HabitacionesModel P_Habitaciones)
        {
                 bool estaOcupado = P_Habitaciones.Ocupado;
                 GestorConexiones objconexion = new GestorConexiones();

                 List<HabitacionesModel> lstresultados = await objconexion.ConsultarHabitacionOcupada(P_Habitaciones);
                 return View(lstresultados);


        }

    }
}
