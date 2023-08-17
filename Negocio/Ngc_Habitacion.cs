﻿using Datos;
using Entidad.Models;
using Microsoft.EntityFrameworkCore;

namespace Negocio
{
    public class Habitacion
    {
        static Datos.DBContext dBContext = DBContext.dBContext;
        public static List<Entidad.Models.Habitacion> getAll()
        {
            List<Entidad.Models.Habitacion> habitaciones = dBContext.Habitacions.ToList();
            foreach (Entidad.Models.Habitacion hbt in habitaciones)
            {
                hbt.IdTipoHabitacionNavigation = TipoHabitacion.getOne(hbt.IdTipoHabitacion);
            }
            return habitaciones;
        }
        public static void Create(Entidad.Models.Habitacion hbt)
        {
            dBContext.Habitacions.Add(hbt);
            dBContext.SaveChanges();
            dBContext.Update(hbt);
        }
        public static bool Delete(Entidad.Models.Habitacion hbt)
        {
            //return Datos.Habitacion.Delete(id);
            if (hbt != null)
            {
                dBContext.Habitacions.Remove(hbt);
                dBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Update(Entidad.Models.Habitacion hbt)
        {
            dBContext.Update(hbt);
            dBContext.SaveChanges();
        }
    }

    public class TipoHabitacion
    {
        static Datos.DBContext dBContext = DBContext.dBContext;
        public static List<Entidad.Models.TipoHabitacion> getAll()
        {
            List<Entidad.Models.TipoHabitacion> lstTipHbt = dBContext.TipoHabitacions.ToList();
            foreach (Entidad.Models.TipoHabitacion tipHbt in lstTipHbt)
            {
                tipHbt.PrecioTipoHabitacions = dBContext.PrecioTipoHabitacions.Where(tip => tip.IdTipoHabitacion == tipHbt.IdTipoHabitacion).ToList();
            }
            return lstTipHbt;
        }
        public static Entidad.Models.TipoHabitacion getOne(int id)
        {
            Entidad.Models.TipoHabitacion tipHbt = dBContext.TipoHabitacions.Find(id);
            if (tipHbt != null)
            {
                tipHbt.PrecioTipoHabitacions = dBContext.PrecioTipoHabitacions.Where(tip => tip.IdTipoHabitacion == tipHbt.IdTipoHabitacion).ToList();
            }
            return tipHbt;
        }
        public static void Create(Entidad.Models.TipoHabitacion tipHbt)
        {
            dBContext.TipoHabitacions.Add(tipHbt);
            dBContext.SaveChanges();
            dBContext.Update(tipHbt);
        }
        public static void Update(Entidad.Models.TipoHabitacion tipHbt)
        {
            dBContext.Update(tipHbt);
            dBContext.SaveChanges();
        }
        public static bool Delete(Entidad.Models.TipoHabitacion tipHbt)
        {
            if (tipHbt != null)
            {
                dBContext.TipoHabitacions.Remove(tipHbt);
                dBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        //Devuelve entidad precioTipoHabitacion de un tipoHabitacion especifico que pertenezca a la fecha ingresada
        public static Entidad.Models.PrecioTipoHabitacion DevPrecioFecha(DateTime fecha, Entidad.Models.TipoHabitacion tipHbt)
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
        static Datos.DBContext dBContext = DBContext.dBContext;
        public static List<Entidad.Models.PrecioTipoHabitacion> getAll()
        {
            List<Entidad.Models.PrecioTipoHabitacion> lstPresTipHbt = dBContext.PrecioTipoHabitacions.ToList();
            foreach (Entidad.Models.PrecioTipoHabitacion PresTipHbt in lstPresTipHbt)
            {
                PresTipHbt.IdTipoHabitacionNavigation = TipoHabitacion.getOne(PresTipHbt.IdTipoHabitacion);
            }
            return lstPresTipHbt;
        }

        public static Entidad.Models.PrecioTipoHabitacion getOne(int id)
        {
            Entidad.Models.PrecioTipoHabitacion PresTipHbt = dBContext.PrecioTipoHabitacions.Find(id);
            if (PresTipHbt != null)
            {
                PresTipHbt.IdTipoHabitacionNavigation = TipoHabitacion.getOne(PresTipHbt.IdTipoHabitacion);
            }
            return PresTipHbt;
        }

        public static void Create(Entidad.Models.PrecioTipoHabitacion PresTipHbt)
        {
            dBContext.PrecioTipoHabitacions.Add(PresTipHbt);
            dBContext.SaveChanges();
            dBContext.Update(PresTipHbt);
        }

        public static void Update(Entidad.Models.PrecioTipoHabitacion PresTipHbt)
        {
            dBContext.Update(PresTipHbt);
            dBContext.SaveChanges();
        }

        public static bool Delete(Entidad.Models.PrecioTipoHabitacion PresTipHbt)
        {
            if (PresTipHbt != null)
            {
                dBContext.PrecioTipoHabitacions.Remove(PresTipHbt);
                dBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}