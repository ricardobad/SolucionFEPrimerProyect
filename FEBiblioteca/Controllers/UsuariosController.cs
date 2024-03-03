using FEBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEBiblioteca.Controllers
{
    public class UsuariosController : Controller
    {

        public async Task<IActionResult> Index()
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<UsuariosModel> lstresultados = await objconexion.ListarUsuarios();
            return View(lstresultados);
        }


        public IActionResult CrearUsuario()
        {
            return View();
        }

   
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult MenuAdministrador()
        {
            return View();
        }
 


        public async Task<IActionResult> InicioSesion(UsuariosModel P_Usuarios)
        {
            ErrorViewModel msj = new ErrorViewModel();
            GestorConexiones objconexion = new GestorConexiones();
            List<UsuariosModel> lstresultados = await objconexion.ConsultarInicioSesion(P_Usuarios);
            lstresultados.FirstOrDefault();
                
            if(lstresultados.Count == 0 )
            {
                msj.RequestId = "Usuario o contrasena no encontrados";
                return RedirectToAction("Login");
            }
            else
            {
                foreach (UsuariosModel usuario in lstresultados)
                {
                    //Arreglar la lista de opciones de cada perfil
                    // crear vista para perfil 2
                    if (usuario.Id_perfil == 2)
                    {
                            return RedirectToAction("Menu");
                    }
                    
                }
                // Crear vista para perfil 1
                return RedirectToAction("MenuAdministrador");
            }
        }

        ///Numero 1 = perfil administrador
        ///Numero 2 =  perfil usuario
        public async Task<IActionResult> Agregar(UsuariosModel P_Usuario)
        {
 
            ErrorViewModel msj = new ErrorViewModel();
            GestorConexiones objconexion = new GestorConexiones();
            List<UsuariosModel> lstresultados = await objconexion.ConsultarUsuarios(new UsuariosModel { Id_Usuario = P_Usuario.Id_Usuario });
            lstresultados.FirstOrDefault();
            if (lstresultados.Count == 0)
            {
                ReportesModel aux = new ReportesModel();
                aux.Reporte = "Se agregó el usuario " + P_Usuario.Id_Usuario + " con fecha " + DateTime.Now;
                await objconexion.Agregar(P_Usuario);
                await objconexion.AgregarReporte(aux);
                return RedirectToAction("Index");
            }
            else
            {
                msj.RequestId = "El usuario tiene una reserva por lo que no se puede eliminar";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Registrarse(UsuariosModel P_Usuario)
        {
            ErrorViewModel msj = new ErrorViewModel();
            GestorConexiones objconexion = new GestorConexiones();
            List<UsuariosModel> lstresultados = await objconexion.ConsultarUsuarios(new UsuariosModel { Id_Usuario = P_Usuario.Id_Usuario });
            lstresultados.FirstOrDefault();
            if (lstresultados.Count == 0)
            {
                ReportesModel aux = new ReportesModel();
                aux.Reporte = "Se registró el usuario " + P_Usuario.Id_Usuario + " con fecha " + DateTime.Now;
                P_Usuario.Id_perfil = 2;
                await objconexion.Agregar(P_Usuario);
                await objconexion.AgregarReporte(aux);
                return RedirectToAction("Login");
            }
            else
            {
                msj.RequestId = "El usuario tiene una reserva por lo que no se puede eliminar";
                return RedirectToAction("Registro");
            }
        }
        public async Task<IActionResult> EliminarUsuario(int pUsuario)
        {
            ErrorViewModel msj = new ErrorViewModel();
            GestorConexiones objconexion = new GestorConexiones();
            List<ReservaModel> lstresultados = await objconexion.ConsultarReservaPorIdUsuario(new ReservaModel { Id_usuario = pUsuario });
            lstresultados.FirstOrDefault();
            if (lstresultados.Count == 0)
            {
                ReportesModel aux = new ReportesModel();
                aux.Reporte = "Se eliminó el usuario " + pUsuario + " con fecha " + DateTime.Now;
                await objconexion.EliminarUsuario(new UsuariosModel { Id_Usuario = pUsuario });
                await objconexion.AgregarReporte(aux);
                return RedirectToAction("Index");
            }
            else
            {
                msj.RequestId = "El usuario tiene una reserva por lo que no se puede eliminar";
                return RedirectToAction("Index");
            }
        }


        public async Task<IActionResult> VerInfoModificar(int pUsuario)
        {
            GestorConexiones objconexion = new GestorConexiones();
            List<UsuariosModel> lstresultados = await objconexion.ConsultarUsuarios(new UsuariosModel { Id_Usuario = pUsuario });
            return View(lstresultados.FirstOrDefault());
        }
 

        public async Task<IActionResult> ModificarUsuario(UsuariosModel P_Usuario)
        {
            ReportesModel aux = new ReportesModel();
            aux.Reporte = "Se modificó el usuario " + P_Usuario.Id_Usuario + " con fecha " + DateTime.Now;
            GestorConexiones objconexion = new GestorConexiones();
            await objconexion.ModificarUsuario(P_Usuario);
            await objconexion.AgregarReporte(aux);
            return RedirectToAction("Index");
        }


    }
}
