using FEBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace FEBiblioteca.Controllers
{
    public class GestorConexiones : Controller
    {
       
        #region Propiedades

        public HttpClient ConexionApi { get; set; }

        #endregion

        #region Constructor

        public GestorConexiones()
        {
            ConexionApi = new HttpClient(); //Inicializa la propiedad para evitar problemas o errores por valor Nulo
            EstablerParametrosBase(); //Establece valores basicos para el objeto que viajara por HTML
        }

        #endregion


        #region Metodos

        #region Privada
        private void EstablerParametrosBase()
        {
            ConexionApi.BaseAddress = new Uri("http://localhost:53895"); // Direccion base del Api a Consultar 
            ConexionApi.DefaultRequestHeaders.Accept.Clear();
            ConexionApi.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));  //Se estable el tipo de formato de los datos a enviar por HTML
        }
        #endregion

        #region Publica


        #region Habitaciones

        //metodo para listar las habitaciones
        public async Task<List<HabitacionesModel>> ListarHabitaciones ()
        {
            List<HabitacionesModel> lista = new List<HabitacionesModel>(); //variable que retornara el resultado de la consulta al Api
            string rutaApi = @"api/Hotel/ConsultarTodasHabitaciones"; //Aqui se estable la ruta a consultar en el Servicio

            //Aqui se aplica la consulta
            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);
            if (resultadoConsumo.IsSuccessStatusCode) //Verifica si la consulta fue positiva, en caso de ser positiva se obtienen los datos y se transforman a un lista de tipo modelo
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<HabitacionesModel>>(jsonstring); //Convierte a entidad el resultado
            }

            return lista;
        }
        //metodo para agregar habitaciones
        public async Task<bool> AgregarHabitaciones(HabitacionesModel P_Habitaciones)
        {
            string rutaApi = @"api/Hotel/AgregarHabitacion"; //Aqui se estable la ruta a consultar en el Servicio
            HttpResponseMessage resultadodelconsumo = await ConexionApi.PostAsJsonAsync(rutaApi, P_Habitaciones);
            return resultadodelconsumo.IsSuccessStatusCode;
        }
        public async Task<List<HabitacionesModel>> ConsultarHabitacion(HabitacionesModel P_Habitaciones)
        {
            List<HabitacionesModel> entidad = new List<HabitacionesModel>(); // Lista para almacenar los resultados de la consulta

            string rutaApi = @"api/Hotel/ConsultarHabitacionesPorId"; // Ruta a consultar en el servicio

            string aux = Convert.ToString(P_Habitaciones.Id_habitacion);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("P_Habitacion", aux);

            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);

            if (resultadoConsumo.IsSuccessStatusCode)
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();

                entidad = JsonConvert.DeserializeObject<List<HabitacionesModel>>(jsonstring);
            }

            return entidad;
        }
        public async Task<bool> EliminarHabitacion(HabitacionesModel P_Habitacion)
        {

            string rutaApi = @"api/Hotel/EliminarHabitacion"; // Aquí se establece la ruta a consultar en el Servicio

            string aux = Convert.ToString(P_Habitacion.Id_habitacion);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pHabitacion", aux);

            HttpResponseMessage resultadodelconsumo = await ConexionApi.DeleteAsync(rutaApi);
            return resultadodelconsumo.IsSuccessStatusCode;


        }
        public async Task<bool> ModificarHabitacion(HabitacionesModel P_Habitacion)
        {

            string rutaApi = @"api/Hotel/ModificarHabitacion"; // Aquí se establece la ruta a consultar en el Servicio

            string aux = Convert.ToString(P_Habitacion.Id_habitacion);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pHabitacion", aux);

            HttpResponseMessage resultadodelconsumo = await ConexionApi.PutAsJsonAsync(rutaApi, P_Habitacion);
            return resultadodelconsumo.IsSuccessStatusCode;


        }

        public async Task<List<HabitacionesModel>> ConsultarHabitacionOcupada(HabitacionesModel P_Habitaciones)
        {

            List<HabitacionesModel> entidad = new List<HabitacionesModel>();

            string rutaApi = @"api/Hotel/ConsultarHabitacionOcupada";
            string aux = Convert.ToString(P_Habitaciones.Ocupado);
            ConexionApi.DefaultRequestHeaders.Add("P_Habitaciones", aux);

            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);

            if (resultadoConsumo.IsSuccessStatusCode)
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();

                entidad = JsonConvert.DeserializeObject<List<HabitacionesModel>>(jsonstring);

            }

            return entidad;
        }

        #endregion


        #region Pagos
        public async Task<List<PagosModel>> ListarPagos()
        {
            List<PagosModel> lista = new List<PagosModel>(); //variable que retornara el resultado de la consulta al Api
            string rutaApi = @"api/Hotel/ConsultarTodosLosPagos"; //Aqui se estable la ruta a consultar en el Servicio

            //Aqui se aplica la consulta
            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);
            if (resultadoConsumo.IsSuccessStatusCode) //Verifica si la consulta fue positiva, en caso de ser positiva se obtienen los datos y se transforman a un lista de tipo modelo
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<PagosModel>>(jsonstring); //Convierte a entidad el resultado
            }

            return lista;
        }
        public async Task<List<PagosModel>> ConsultarPago(PagosModel P_Pago)
        {
            List<PagosModel> entidad = new List<PagosModel>(); // Lista para almacenar los resultados de la consulta

            string aux = Convert.ToString(P_Pago.Id_pago);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("P_Pago", aux);
            string rutaApi = @"api/Hotel/ConsultarPagosPorId"; // Ruta a consultar en el servicio

            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);

            if (resultadoConsumo.IsSuccessStatusCode)
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();

                entidad = JsonConvert.DeserializeObject<List<PagosModel>>(jsonstring);
            }

            return entidad;
        }
        //metodo para agregar Pagos
        public async Task<bool> AgregarPagos(PagosModel P_Pagos)
        {
            string rutaApi = @"api/Hotel/AgregarPagos"; //Aqui se estable la ruta a consultar en el Servicio
            HttpResponseMessage resultadodelconsumo = await ConexionApi.PostAsJsonAsync(rutaApi, P_Pagos);
            return resultadodelconsumo.IsSuccessStatusCode;
        }
        public async Task<bool> EliminarPagos(PagosModel P_Pagos)
        {
          
            string rutaApi = @"api/Hotel/EliminarPagos"; // Aquí se establece la ruta a consultar en el Servicio

            string aux = Convert.ToString(P_Pagos.Id_pago);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pPagos", aux);

            HttpResponseMessage resultadodelconsumo = await ConexionApi.DeleteAsync(rutaApi);
            return resultadodelconsumo.IsSuccessStatusCode;

        
        }
        public async Task<bool> ModificarPagos(PagosModel P_Pagos)
        {

            string rutaApi = @"api/Hotel/ModificarPagos"; // Aquí se establece la ruta a consultar en el Servicio

            string aux = Convert.ToString(P_Pagos.Id_pago);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pPagos", aux);
            
            HttpResponseMessage resultadodelconsumo = await ConexionApi.PutAsJsonAsync(rutaApi, P_Pagos);
            return resultadodelconsumo.IsSuccessStatusCode;


        }

        #endregion


        #region Reserva

        //metodo para listar las reservas
        public async Task<List<ReservaModel>> ListarReserva()
        {
            List<ReservaModel> lista = new List<ReservaModel>(); //variable que retornara el resultado de la consulta al Api
            string rutaApi = @"api/Hotel/ConsultarTodasLasReservas"; //Aqui se estable la ruta a consultar en el Servicio

            //Aqui se aplica la consulta
            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);
            if (resultadoConsumo.IsSuccessStatusCode) //Verifica si la consulta fue positiva, en caso de ser positiva se obtienen los datos y se transforman a un lista de tipo modelo
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<ReservaModel>>(jsonstring); //Convierte a entidad el resultado
            }

            return lista;
        }
        //metodo para agregar reservas
        public async Task<bool> AgregarReservas(ReservaModel P_Reservas)
        {
            string rutaApi = @"api/Hotel/AgregarReserva"; //Aqui se estable la ruta a consultar en el Servicio
            HttpResponseMessage resultadodelconsumo = await ConexionApi.PostAsJsonAsync(rutaApi, P_Reservas);
            return resultadodelconsumo.IsSuccessStatusCode;
        }
        public async Task<List<ReservaModel>> ConsultarReserva(ReservaModel P_Reserva)
        {
            List<ReservaModel> entidad = new List<ReservaModel>(); // Lista para almacenar los resultados de la consulta

            string aux = Convert.ToString(P_Reserva.Id_reserva);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("P_Reserva", aux);
            string rutaApi = @"api/Hotel/ConsultarReservaPorId"; // Ruta a consultar en el servicio

            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);

            if (resultadoConsumo.IsSuccessStatusCode)
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();

                entidad = JsonConvert.DeserializeObject<List<ReservaModel>>(jsonstring);
            }

            return entidad;
        }

        public async Task<List<ReservaModel>> ConsultarReservaPorIdUsuario(ReservaModel P_Reserva)
        {

            List<ReservaModel> entidad = new List<ReservaModel>(); // Lista para almacenar los resultados de la consulta

            string aux = Convert.ToString(P_Reserva.Id_usuario);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("P_Reserva", aux);
            string rutaApi = @"api/Hotel/ConsultarReservaPorIdUsuario"; // Ruta a consultar en el servicio

            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);

            if (resultadoConsumo.IsSuccessStatusCode)
            {

                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();

                entidad = JsonConvert.DeserializeObject<List<ReservaModel>>(jsonstring);
            }


            return entidad;

        }

        public async Task<bool> EliminarReserva(ReservaModel P_Reserva)
        {

            string rutaApi = @"api/Hotel/EliminarReserva"; // Aquí se establece la ruta a consultar en el Servicio

            string aux = Convert.ToString(P_Reserva.Id_reserva);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pReserva", aux);

            HttpResponseMessage resultadodelconsumo = await ConexionApi.DeleteAsync(rutaApi);
            return resultadodelconsumo.IsSuccessStatusCode;


        }
        public async Task<bool> ModificarReserva(ReservaModel P_Reserva)
        {

            string rutaApi = @"api/Hotel/ModificarReserva"; // Aquí se establece la ruta a consultar en el Servicio

            string aux = Convert.ToString(P_Reserva.Id_reserva);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pReserva", aux);

            HttpResponseMessage resultadodelconsumo = await ConexionApi.PutAsJsonAsync(rutaApi, P_Reserva);
            return resultadodelconsumo.IsSuccessStatusCode;


        }
        #endregion


        #region Usuarios

        //metodo para listar los Usuarios
        public async Task<List<UsuariosModel>> ListarUsuarios()
        {
            List<UsuariosModel> lista = new List<UsuariosModel>(); //variable que retornara el resultado de la consulta al Api
            string rutaApi = @"api/Hotel/ConsultarTodosUsuarios"; //Aqui se estable la ruta a consultar en el Servicio

            //Aqui se aplica la consulta
            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);
            if (resultadoConsumo.IsSuccessStatusCode) //Verifica si la consulta fue positiva, en caso de ser positiva se obtienen los datos y se transforman a un lista de tipo modelo
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<UsuariosModel>>(jsonstring); //Convierte a entidad el resultado
            }

            return lista;
        }
        //metodo para agregar usuarios
        public async Task<bool> Agregar(UsuariosModel P_Usuario)
        {
            string rutaApi = @"api/Hotel/Agregar"; //Aqui se estable la ruta a consultar en el Servicio
            HttpResponseMessage resultadodelconsumo = await ConexionApi.PostAsJsonAsync(rutaApi, P_Usuario);
            return resultadodelconsumo.IsSuccessStatusCode;
        }
        public async Task<List<UsuariosModel>> ConsultarUsuarios(UsuariosModel P_Usuario)
        {
            List<UsuariosModel> entidad = new List<UsuariosModel>(); // Lista para almacenar los resultados de la consulta

            string aux = Convert.ToString(P_Usuario.Id_Usuario);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pUsuario", aux);
            string rutaApi = @"api/Hotel/ConsultarUsuariosPorID"; // Ruta a consultar en el servicio

            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);

            if (resultadoConsumo.IsSuccessStatusCode)
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();

                entidad = JsonConvert.DeserializeObject<List<UsuariosModel>>(jsonstring);
            }

            return entidad;
        }


        public async Task<List<UsuariosModel>> ConsultarInicioSesion(UsuariosModel pUsuarios)
        {

            List<UsuariosModel> entidad = new List<UsuariosModel>(); // Lista para almacenar los resultados de la consulta

            string aux = Convert.ToString(pUsuarios.Id_Usuario);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pUsuario", aux );
            ConexionApi.DefaultRequestHeaders.Add("pOtro", pUsuarios.Contrasena);

            string rutaApi = @"api/Hotel/ConsultarInicioSesion"; // Ruta a consultar en el servicio

            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);

            if (resultadoConsumo.IsSuccessStatusCode)
            {

                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();

                entidad = JsonConvert.DeserializeObject<List<UsuariosModel>>(jsonstring);
            }


            return entidad;

        }
        public async Task<bool> EliminarUsuario(UsuariosModel P_Usuario)
        {
            
                string rutaApi = @"api/Hotel/EliminarUsuario"; // Aquí se establece la ruta a consultar en el Servicio

                string aux = Convert.ToString(P_Usuario.Id_Usuario);
                //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
                ConexionApi.DefaultRequestHeaders.Add("pUsuario", aux);

                HttpResponseMessage resultadodelconsumo = await ConexionApi.DeleteAsync(rutaApi);
                return resultadodelconsumo.IsSuccessStatusCode;
           


        }
        public async Task<bool> ModificarUsuario(UsuariosModel P_Usuario)
        {

            string rutaApi = @"api/Hotel/ModificarUsuario"; // Aquí se establece la ruta a consultar en el Servicio

            string aux = Convert.ToString(P_Usuario.Id_Usuario);
            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pUsuario", aux);

            HttpResponseMessage resultadodelconsumo = await ConexionApi.PutAsJsonAsync(rutaApi, P_Usuario);
            return resultadodelconsumo.IsSuccessStatusCode;


        }
        #endregion


        #region Reportes

        //metodo para listar los reportes
        public async Task<List<ReportesModel>> ListaReportes()
        {
            List<ReportesModel> lista = new List<ReportesModel>(); //variable que retornara el resultado de la consulta al Api
            string rutaApi = @"api/Hotel/ConsultarTodosReportes"; //Aqui se estable la ruta a consultar en el Servicio

            //Aqui se aplica la consulta
            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);
            if (resultadoConsumo.IsSuccessStatusCode) //Verifica si la consulta fue positiva, en caso de ser positiva se obtienen los datos y se transforman a un lista de tipo modelo
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();
                lista = JsonConvert.DeserializeObject<List<ReportesModel>>(jsonstring); //Convierte a entidad el resultado
            }

            return lista;
        }



        //metodo para agregar usuarios

        public async Task<bool> AgregarReporte(ReportesModel P_Reporte)
        {
            string rutaApi = @"api/Hotel/AgregarReporte"; //Aqui se estable la ruta a consultar en el Servicio
            HttpResponseMessage resultadodelconsumo = await ConexionApi.PostAsJsonAsync(rutaApi, P_Reporte);
            return resultadodelconsumo.IsSuccessStatusCode;
        }

        public async Task<ReportesModel> ConsultarReportePorID(ReportesModel P_Reporte)
        {
            ReportesModel entidad = new ReportesModel(); //variable que retornara el resultado de la consulta al Api
            string rutaApi = @"api/Hotel/ConsultarReportesPorID"; //Aqui se estable la ruta a consultar en el Servicio

            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("pReporte", P_Reporte.id_reporte);

            //Aqui se aplica la consulta
            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);
            if (resultadoConsumo.IsSuccessStatusCode) //Verifica si la consulta fue positiva, en caso de ser positiva se obtienen los datos y se transforman a un lista de tipo modelo
            {
                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();
                entidad = JsonConvert.DeserializeObject<ReportesModel>(jsonstring); //Convierte a entidad el resultado
            }

            return entidad;
        }

        public async Task<List<PagosModel>> ConsultarMontoTotal(PagosModel P_Pago)
        {
            List<PagosModel> entidad = new List<PagosModel>(); // Lista para almacenar los resultados de la consulta

            string fechaInicio = P_Pago.Fecha_pago.ToString("yyyy/MM/dd");
            string fechaFin = P_Pago.Fecha_Fin.ToString("yyyy/MM/dd");

            //Aqui se establece en el encabezado(FromHeader) el parametro de la consulta
            ConexionApi.DefaultRequestHeaders.Add("FechaInicio",fechaInicio);
            ConexionApi.DefaultRequestHeaders.Add("FechaFin",fechaFin);

            string rutaApi = @"api/Hotel/ConsultarVenta"; // Ruta a consultar en el servicio

            HttpResponseMessage resultadoConsumo = await ConexionApi.GetAsync(rutaApi);

            if (resultadoConsumo.IsSuccessStatusCode)
            {

                string jsonstring = await resultadoConsumo.Content.ReadAsStringAsync();

                entidad = JsonConvert.DeserializeObject<List<PagosModel>>(jsonstring);
            }
            else
            {
                // Procesar el error y mostrar el contenido del mensaje de error
                string contenidoError = await resultadoConsumo.Content.ReadAsStringAsync();
                // Aquí puedes mostrar o registrar el contenido del mensaje de error para inspeccionar el problema
                Console.WriteLine("Error en la solicitud: " + contenidoError);
            }

            return entidad;
        }

        #endregion


        #endregion

        #endregion
    }
}
