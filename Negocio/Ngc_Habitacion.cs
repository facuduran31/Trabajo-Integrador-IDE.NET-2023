using Datos;
using Entidad.Api;
using Entidad.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Negocio
{
    public class Habitacion
    {
        static readonly DBContext dBContext = DBContext.dBContext;
        static readonly string defaultUrl = Conexion.defaultUrl + "Habitacion/";
        public static async Task<Entidad.Models.Habitacion?> GetOne(int id)
        {
            Task<Entidad.Models.Habitacion?> task = Conexion.http.GetFromJsonAsync<Entidad.Models.Habitacion>(defaultUrl + "GetOne/" + id);
            Entidad.Models.Habitacion? hbt = await task;
            if (hbt != null)
            {
                await Initialize(hbt);
            }
            return hbt;
        }

        public static async Task<List<Entidad.Models.Habitacion>> GetAll()
        {
            List<Entidad.Models.Habitacion> response = (await Conexion.http.GetFromJsonAsync<List<Entidad.Models.Habitacion>>(defaultUrl + "GetAll"))!;
            foreach (Entidad.Models.Habitacion hbt in response!)
            {
                await Initialize(hbt);
            }
            return response!;
        }

        public static async Task<Entidad.Models.Habitacion?> Create(Entidad.Models.Habitacion hbt)
        {
            HabitacionApi hbtApi = GetApi(hbt);
            var result = await Conexion.http.PostAsJsonAsync(defaultUrl + "Create", hbtApi);
            if (result.IsSuccessStatusCode)
            {
                int id = JsonConvert.DeserializeObject<int>(await result.Content.ReadAsStringAsync())!;
                Entidad.Models.Habitacion createdHbt = (await GetOne(id))!;
                return createdHbt;
            }
            else { return null; }
        }
        public static async Task<bool> Delete(Entidad.Models.Habitacion hbt)
        {
            var result = await Conexion.http.DeleteAsync(defaultUrl + "Delete/" + hbt.IdHabitacion);
            return result.IsSuccessStatusCode;
        }

        public static async Task<bool> Update(Entidad.Models.Habitacion hbt)
        {
            Entidad.Api.HabitacionApi hbtApi = GetApi(hbt);
            var result = await Conexion.http.PutAsJsonAsync(defaultUrl + "Update/" + hbtApi.IdHabitacion, hbtApi);
            return result.IsSuccessStatusCode;
        }

        /// <summary></summary>
        /// <returns>Lista de habitaciones dadas de alta (hbt.estado == true)</returns>
        public static async Task<List<Entidad.Models.Habitacion>> GetAllEnabled()
        {
            List<Entidad.Models.Habitacion> response = (await Conexion.http.GetFromJsonAsync<List<Entidad.Models.Habitacion>>(defaultUrl + "GetAllEnabled"))!;
            foreach (Entidad.Models.Habitacion hbt in response!)
            {
                await Initialize(hbt);
            }
            return response!;
        }

        /// <summary></summary>
        /// <param name="start">fecha de inicio</param>
        /// <param name="end">fecha de final</param>
        /// <returns>Lista de habitaciones disponibles para un intervalo de fechas</returns>
        public static async Task<List<Entidad.Models.Habitacion>> GetAvailableBetween(DateTime start, DateTime end)
        {
            return TakeAvailable(await GetAllEnabled(), start, end);
        }

        /// <summary>
        /// Toma una lista de habitaciones y devuelve las habitaciones disponibles (sin reservas "En espera" o "En curso") para un intervalo de fechas
        /// </summary>
        /// <param name="lstHbt">listado de habitaciones a filtrar</param>
        /// <param name="start">fecha de inicio</param>
        /// <param name="end">fecha de fin</param>
        /// <returns>Listado de habitaciones disponibles</returns>
        public static List<Entidad.Models.Habitacion> TakeAvailable(List<Entidad.Models.Habitacion> lstHbt, DateTime start, DateTime end)
        {
            return lstHbt.FindAll(hbt =>
                hbt.Reservas.ToList().Find(rsv =>
                    (rsv.EstadoReserva == "En espera" || rsv.EstadoReserva == "En curso") && !(rsv.FechaInicioReserva.CompareTo(end) >= 0 || rsv.FechaFinReserva.CompareTo(start) <= 0)
                ) == null || hbt.Reservas.Count == 0
            );
        }

        /// <summary></summary>
        /// <param name="amountPeople">capacidad de personas (minima) deseada</param>
        /// <returns>Listado de habitaciones con capacidad igual o mayor de personas</returns>
        public static async Task<List<Entidad.Models.Habitacion>> GetForAmountOfPeople(int amountPeople)
        {
            Task<List<Entidad.Models.Habitacion>> getAmount = Conexion.http.GetFromJsonAsync<List<Entidad.Models.Habitacion>>(defaultUrl + "GetForAmountOfPeople/" + amountPeople)!;
            List<Entidad.Models.Habitacion> response = (await getAmount)!;
            if (getAmount.IsCompletedSuccessfully)
            {
                foreach (Entidad.Models.Habitacion hbt in response)
                {
                    await Initialize(hbt);
                }
                return response;
            }
            else
            {
                throw new Exception();
            }

        }
        /*public static List<Entidad.Models.Habitacion> GetByPiso(int piso)
        {
            return GetAll().FindAll(hbt => hbt.PisoHabitacion == piso);
        }
        public static List<Entidad.Models.Habitacion> GetByNro(int nro)
        {
            return GetAll().FindAll(hbt => hbt.NumeroHabitacion == nro);
        }
        public static Entidad.Models.Habitacion? GetByPiso_Nro(int piso, int nro)
        {
            return GetAll().Find(hbt => hbt.PisoHabitacion == piso && hbt.NumeroHabitacion == nro);
        }*/

        public static async Task Initialize(Entidad.Models.Habitacion hbt)
        {
            Task<Entidad.Models.TipoHabitacion> getOne = TipoHabitacion.GetOne(hbt.IdTipoHabitacion)!;
            hbt.IdTipoHabitacionNavigation = await getOne;

            Task<List<Entidad.Models.Reserva>> getRsvOfHbt = Conexion.http.GetFromJsonAsync<List<Entidad.Models.Reserva>>(Conexion.defaultUrl + "Reserva/GetAllOfHabitacion/" + hbt.IdHabitacion)!;
            List<Entidad.Models.Reserva> rsv = await getRsvOfHbt;

            if (rsv == null)
            {
                hbt.Reservas = new List<Entidad.Models.Reserva>();
            }
            else
            {
                hbt.Reservas = rsv;
            }
        }

        public static Entidad.Api.HabitacionApi GetApi(Entidad.Models.Habitacion hbt)
        {
            Entidad.Api.HabitacionApi api = new Entidad.Api.HabitacionApi();
            api.IdHabitacion = hbt.IdHabitacion;
            api.Estado = hbt.Estado;
            api.NumeroHabitacion = hbt.NumeroHabitacion;
            api.PisoHabitacion = hbt.PisoHabitacion;
            api.IdTipoHabitacion = hbt.IdTipoHabitacion;
            return api;
        }
    }

    public class TipoHabitacion
    {
        static readonly string defaultUrl = Conexion.defaultUrl + "TipoHabitacion/";
        public static async Task<List<Entidad.Models.TipoHabitacion>> GetAll()
        {
            var response = await Conexion.http.GetStringAsync(defaultUrl + "GetAll");
            var data = JsonConvert.DeserializeObject<List<Entidad.Models.TipoHabitacion>>(response);
            foreach (Entidad.Models.TipoHabitacion tp in data)
            {
                tp.PrecioTipoHabitacions = await PrecioTipoHabitacion.GetAllById(tp.IdTipoHabitacion);
            }
            return data;

        }
        public static async Task<Entidad.Models.TipoHabitacion> GetOne(int id)
        {
            var response = await Conexion.http.GetStringAsync(defaultUrl + "GetOne/" + id.ToString());
            var data = JsonConvert.DeserializeObject<Entidad.Models.TipoHabitacion>(response);
            data!.PrecioTipoHabitacions = await PrecioTipoHabitacion.GetAllById(id);
            return data;
        }
        public static async Task<Entidad.Models.TipoHabitacion> Create(Entidad.Models.TipoHabitacion tipHbt)
        {
            Entidad.Api.TipoHabitacionApi tipApi = GetApiTipo(tipHbt);
            var response = await Conexion.http.PostAsJsonAsync(defaultUrl + "Create", tipApi);
            int data = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
            Entidad.Models.TipoHabitacion createdTipo = (await GetOne(data))!;
            return createdTipo;
        }
        public static async Task<bool> Update(Entidad.Models.TipoHabitacion tipHbt)
        {
            Entidad.Api.TipoHabitacionApi tipApi = GetApiTipo(tipHbt);
            var response = await Conexion.http.PutAsJsonAsync(defaultUrl + "Update/" + tipHbt.IdTipoHabitacion.ToString(), tipApi);
            return response.IsSuccessStatusCode;
        }
        public static async Task<bool> Delete(Entidad.Models.TipoHabitacion tipHbt)
        {
            var result = await Conexion.http.DeleteAsync(defaultUrl + "Delete/" + tipHbt.IdTipoHabitacion.ToString());
            return result.IsSuccessStatusCode;
        }

        public static Entidad.Api.TipoHabitacionApi GetApiTipo(Entidad.Models.TipoHabitacion thbt)
        {
            Entidad.Api.TipoHabitacionApi api = new Entidad.Api.TipoHabitacionApi();
            api.IdTipoHabitacion = thbt.IdTipoHabitacion;
            api.Descripcion = thbt.Descripcion;
            api.NumeroCamas = thbt.NumeroCamas;
            return api;
        }

        /// <summary>
        /// </summary>
        /// <param name="fecha">fecha a buscar</param>
        /// <param name="tipHbt">tipoHabitacion de precio a buscar</param>
        /// <returns>Entidad precioTipoHabitacion de un tipoHabitacion que pertenezca a la fecha ingresada</returns>
        public static Entidad.Models.PrecioTipoHabitacion? DevPrecioFecha(DateTime fecha, Entidad.Models.TipoHabitacion tipHbt)
        {
            Entidad.Models.PrecioTipoHabitacion? precioBuscado = null;
            foreach (var preTipHbt in tipHbt.PrecioTipoHabitacions)
            {
                if (precioBuscado == null && preTipHbt.FechaPrecio <= fecha)
                {
                    precioBuscado = preTipHbt;
                }
                else if (precioBuscado != null)
                {
                    if (preTipHbt.FechaPrecio > precioBuscado.FechaPrecio && preTipHbt.FechaPrecio <= fecha)
                    {
                        precioBuscado = preTipHbt;
                    }
                }
            }
            return precioBuscado;
        }

    }

    public class PrecioTipoHabitacion
    {
        static readonly string defaultUrl = Conexion.defaultUrl + "PrecioTipoHabitacion/";
        public static async Task<List<Entidad.Models.PrecioTipoHabitacion>> GetAll()
        {
            var response = await Conexion.http.GetStringAsync(defaultUrl + "GetAll");
            var data = JsonConvert.DeserializeObject<List<Entidad.Models.PrecioTipoHabitacion>>(response);
            return data;
        }

        public static async Task<Entidad.Models.PrecioTipoHabitacion> GetOne(int id)
        {
            var response = await Conexion.http.GetStringAsync(defaultUrl + "GetOne/" + id.ToString());
            var data = JsonConvert.DeserializeObject<Entidad.Models.PrecioTipoHabitacion>(response);
            return data;
        }

        public static async Task<Entidad.Models.PrecioTipoHabitacion> Create(Entidad.Models.PrecioTipoHabitacion PresTipHbt)
        {
            Entidad.Api.PrecioTipoHabitacionApi srv = GetApiPrecTipo(PresTipHbt);
            var response = await Conexion.http.PostAsJsonAsync(defaultUrl + "Create", srv);
            var data = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
            Entidad.Models.PrecioTipoHabitacion createdServ = (await GetOne(data))!;
            return createdServ;
        }

        public static async Task<List<Entidad.Models.PrecioTipoHabitacion>> GetAllById(int idTipo)
        {
            var response = await Conexion.http.GetStringAsync(defaultUrl + "GetAllOfTipoHabitacion/" + idTipo.ToString());
            var data = JsonConvert.DeserializeObject<List<Entidad.Models.PrecioTipoHabitacion>>(response);
            return data!;
        }


        public static Entidad.Api.PrecioTipoHabitacionApi GetApiPrecTipo(Entidad.Models.PrecioTipoHabitacion precTip)
        {
            Entidad.Api.PrecioTipoHabitacionApi api = new Entidad.Api.PrecioTipoHabitacionApi();
            api.IdPrecioTipoHabitacion = precTip.IdPrecioTipoHabitacion;
            api.FechaPrecio = precTip.FechaPrecio;
            api.IdTipoHabitacion = precTip.IdTipoHabitacion;
            api.PrecioHabitacion = precTip.PrecioHabitacion;
            return api;
        }
    }
}