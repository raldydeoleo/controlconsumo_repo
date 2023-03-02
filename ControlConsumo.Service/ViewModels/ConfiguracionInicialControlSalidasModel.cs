using ControlConsumo.Service.Models.ControlConsumo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class ConfiguracionInicialControlSalidasModel
    {
        public int Id { get; set; }
        public string IdProducto { get; set; }
        public string IdEquipo { get; set; }
        public DateTime FechaProduccion { get; set; }
        public int Turno { get; set; }
        public double CantidadConsumoPendiente { get; set; }
        public string Unidad { get; set; }
        public DateTime? FechaLectura { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estatus { get; set; }
        public static implicit operator ConfiguracionInicialControlSalidasModel(ConfiguracionInicialControlSalida configuracionInicialControlSalida)
        {
            var configuracionInicialControlSalidasModel = new ConfiguracionInicialControlSalidasModel
            {
                Id = configuracionInicialControlSalida.Id,
                IdProducto = configuracionInicialControlSalida.IdProducto,
                IdEquipo = configuracionInicialControlSalida.IdEquipo,
                FechaProduccion = configuracionInicialControlSalida.FechaProduccion,
                Turno = configuracionInicialControlSalida.Turno,
                CantidadConsumoPendiente = configuracionInicialControlSalida.CantidadConsumoPendiente,
                Unidad = configuracionInicialControlSalida.Unidad,
                FechaLectura = configuracionInicialControlSalida.FechaLectura,
                FechaRegistro = configuracionInicialControlSalida.FechaRegistro,
                UsuarioRegistro = configuracionInicialControlSalida.UsuarioRegistro,
                FechaModificacion = configuracionInicialControlSalida.FechaModificacion,
                UsuarioModificacion = configuracionInicialControlSalida.UsuarioModificacion,
                Estatus = configuracionInicialControlSalida.Estatus
            };
            return configuracionInicialControlSalidasModel;
        }
    }
}