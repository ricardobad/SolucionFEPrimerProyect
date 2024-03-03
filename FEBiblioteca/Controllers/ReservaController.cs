using FEBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEBiblioteca.Controllers
{
    public class ReservaController : Controller
    {

        public async Task<IActionResult> Index(int BuscarUsuario, int BuscarCodigo)
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<ReservaModel> resultado = await objconexion.ListarReserva();

            //Aqui se aplica filtrado sobre la lista a mostrar
            if (BuscarUsuario >0)
                resultado = resultado.Where(item => item.Id_usuario.Equals(BuscarUsuario)).ToList();
            else if(BuscarCodigo>0)
                resultado = resultado.Where(item => item.Id_reserva.Equals(BuscarCodigo)).ToList();

            return View(resultado);
        }

        public async Task<IActionResult> Indexa(int BuscarUsuario, int BuscarCodigo)
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<ReservaModel> resultado = await objconexion.ListarReserva();

            //Aqui se aplica filtrado sobre la lista a mostrar
            if (BuscarUsuario > 0)
                resultado = resultado.Where(item => item.Id_usuario.Equals(BuscarUsuario)).ToList();
            else if (BuscarCodigo > 0)
                resultado = resultado.Where(item => item.Id_reserva.Equals(BuscarCodigo)).ToList();

            return View(resultado);
        }

        public IActionResult CrearReservaa()
         {
             return View();
         }

        public IActionResult CrearReserva()
        {
            return View();
        }

        public async Task<IActionResult> GuardarReserva(ReservaModel P_Reservas)
        {
            ErrorViewModel msj = new ErrorViewModel();
            GestorConexiones objconexion = new GestorConexiones();
            List<ReservaModel> lstresultados = await objconexion.ConsultarReserva(new ReservaModel { Id_reserva = P_Reservas.Id_reserva });
            lstresultados.FirstOrDefault();
            if (lstresultados.Count == 0)
            {
                ReportesModel aux = new ReportesModel();
                aux.Reporte = "Se agregó la reserva "+ P_Reservas.Id_reserva+ " con fecha "+DateTime.Now;
                await objconexion.AgregarReservas(P_Reservas);
                await objconexion.AgregarReporte(aux);
                return RedirectToAction("Index");
            }
            else
            {
                msj.RequestId = "El usuario tiene una reserva por lo que no se puede eliminar";
                return RedirectToAction("Index");
            }
          
           
        }
        public async Task<IActionResult> GuardarReservaa(ReservaModel P_Reservas)
        {
            ErrorViewModel msj = new ErrorViewModel();
            GestorConexiones objconexion = new GestorConexiones();
            List<ReservaModel> lstresultados = await objconexion.ConsultarReserva(new ReservaModel { Id_reserva = P_Reservas.Id_reserva });
            lstresultados.FirstOrDefault();
            if (lstresultados.Count == 0)
            {
                ReportesModel aux = new ReportesModel();
                aux.Reporte = "Se agregó la reserva " + P_Reservas.Id_reserva + " con fecha " + DateTime.Now;
                await objconexion.AgregarReservas(P_Reservas);
                await objconexion.AgregarReporte(aux);
                return RedirectToAction("Indexa");
            }
            else
            {
                msj.RequestId = "El usuario tiene una reserva por lo que no se puede eliminar";
                return RedirectToAction("Indexa");
            }


        }

        public async Task<IActionResult> EliminarReserva(int pReserva)
        {
            ReportesModel aux = new ReportesModel();
            aux.Reporte = "Se eliminó la reserva " + pReserva + " con fecha " + DateTime.Now;
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.EliminarReserva(new ReservaModel { Id_reserva = pReserva });
            await objconexion.AgregarReporte(aux);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EliminarReservaa(int pReserva)
        {
            ReportesModel aux = new ReportesModel();
            aux.Reporte = "Se eliminó la reserva " + pReserva + " con fecha " + DateTime.Now;
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.EliminarReserva(new ReservaModel { Id_reserva = pReserva });
            await objconexion.AgregarReporte(aux);
            return RedirectToAction("Indexa");
        }
        public async Task<IActionResult> VerInfoModificar(int pReserva)
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<ReservaModel> lstresultados = await objconexion.ConsultarReserva(new ReservaModel { Id_reserva = pReserva });
            return View(lstresultados.FirstOrDefault());
        }

        public async Task<IActionResult> VerInfoModificara(int pReserva)
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<ReservaModel> lstresultados = await objconexion.ConsultarReserva(new ReservaModel { Id_reserva = pReserva });
            return View(lstresultados.FirstOrDefault());
        }

        public async Task<IActionResult> ModificarReserva(ReservaModel P_Reserva)
        {
            ReportesModel aux = new ReportesModel();
            aux.Reporte = "Se modificó la reserva " + P_Reserva.Id_reserva + " con fecha " + DateTime.Now;
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.ModificarReserva(P_Reserva);
            await objconexion.AgregarReporte(aux);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ModificarReservaa(ReservaModel P_Reserva)
        {
            ReportesModel aux = new ReportesModel();
            aux.Reporte = "Se modificó la reserva " + P_Reserva.Id_reserva + " con fecha " + DateTime.Now;
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.ModificarReserva(P_Reserva);
            await objconexion.AgregarReporte(aux);
            return RedirectToAction("Indexa");
        }

    }
}
