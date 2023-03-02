using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class EstatusBandejaViewModel
    {
        public string mandt { get; set; }
        public string idBandeja { get; set; }
        public int zsecuencia { get; set; }
        public string status { get; set; }
        public string idEquipo { get; set; }
        public string idProcess { get; set; }
        public string idTiempo { get; set; }
        public string matnr { get; set; }
        public string verid { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string usuarioLlenada { get; set; }
        public int secSalida { get; set; }
        public string batchId { get; set; }
        public double menge { get; set; }
        public string meins { get; set; }
        public string cpudt { get; set; }
        public string cputm { get; set; }
        public string fechaVaciada { get; set; }
        public string horaVaciada { get; set; }
        public string idEquipoVaciado { get; set; }
        public string usuarioVaciada { get; set; }
        public string formaVaciada { get; set; }
        public Boolean esUnaDevolucion { get; set; }


        public static implicit operator EstatusBandeja(EstatusBandejaViewModel estatusBandejaVM)
        {
            TimeSpan horaLlenada, horaVaciada;
            DateTime fechaLlenada, fechaVaciada;
            if (!estatusBandejaVM.esUnaDevolucion)
            {
                ValidacionEstatusBandeja(estatusBandejaVM.idBandeja, estatusBandejaVM.zsecuencia, estatusBandejaVM.status);
            }
            bool esHoraLlenadaValida = TimeSpan.TryParseExact(estatusBandejaVM.hora, "hhmmss", System.Globalization.CultureInfo.InvariantCulture, out horaLlenada);
            bool esFechaLlenadaValida = DateTime.TryParseExact(estatusBandejaVM.fecha, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out fechaLlenada);
            bool esHoraVaciadaValida = TimeSpan.TryParseExact(estatusBandejaVM.horaVaciada, "hhmmss", System.Globalization.CultureInfo.InvariantCulture, out horaVaciada);
            bool esFechaVaciadaValida = DateTime.TryParseExact(estatusBandejaVM.fechaVaciada, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out fechaVaciada);
            EstatusBandeja bandejaRegistrada = GetBandejaRegistrada(estatusBandejaVM.idBandeja, estatusBandejaVM.zsecuencia);
            EstatusBandeja estatusBandeja = new EstatusBandeja();
            estatusBandeja.idBandeja = estatusBandejaVM.idBandeja;
            estatusBandeja.secuenciaBandeja = estatusBandejaVM.zsecuencia;
            estatusBandeja.estatus = estatusBandejaVM.status;
            estatusBandeja.idEquipoLlenado = estatusBandejaVM.idEquipo;
            estatusBandeja.idProceso = estatusBandejaVM.idProcess;
            estatusBandeja.idTiempo = estatusBandejaVM.idTiempo == null ? "" : estatusBandejaVM.idTiempo;
            estatusBandeja.idMaterial = estatusBandejaVM.matnr == null ? "" : estatusBandejaVM.matnr;
            estatusBandeja.versionFabricacion = estatusBandejaVM.verid == null ? "" : estatusBandejaVM.verid;
            estatusBandeja.fechaLlenada = esFechaLlenadaValida ? fechaLlenada : (DateTime?)null;
            estatusBandeja.horaLlenada = esHoraLlenadaValida ? horaLlenada : TimeSpan.Zero;
            estatusBandeja.usuarioLlenada = estatusBandejaVM.usuarioLlenada;
            estatusBandeja.secuenciaSalida = estatusBandejaVM.secSalida;
            estatusBandeja.batchId = estatusBandejaVM.batchId == null ? "" : estatusBandejaVM.batchId;
            estatusBandeja.cantidad = estatusBandejaVM.menge;
            estatusBandeja.UMP = estatusBandejaVM.meins == null ? "" : estatusBandejaVM.meins;
            estatusBandeja.fechaRegistro = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            estatusBandeja.horaRegistro = TimeSpan.ParseExact(DateTime.Now.ToString("HHmmss"), "hhmmss", System.Globalization.CultureInfo.InvariantCulture);
            estatusBandeja.fechaVaciada = esFechaVaciadaValida ? fechaVaciada : (DateTime?)null;
            estatusBandeja.horaVaciada = esHoraVaciadaValida ? horaVaciada : TimeSpan.Zero;
            estatusBandeja.idEquipoVaciado = estatusBandejaVM.idEquipoVaciado;
            estatusBandeja.usuarioVaciada = estatusBandejaVM.usuarioVaciada;
            estatusBandeja.formaVaciada = estatusBandejaVM.formaVaciada;
            return estatusBandeja;
        }

        public static implicit operator EstatusBandejaViewModel(EstatusBandeja estatusBandeja)
        {
            return new EstatusBandejaViewModel()
            {
                idBandeja = estatusBandeja.idBandeja,
                zsecuencia = estatusBandeja.secuenciaBandeja,
                status = estatusBandeja.estatus,
                idEquipo = estatusBandeja.idEquipoLlenado,
                idProcess = estatusBandeja.idProceso,
                idTiempo = estatusBandeja.idTiempo,
                matnr = estatusBandeja.idMaterial,
                verid = estatusBandeja.versionFabricacion,
                fecha = estatusBandeja.fechaLlenada?.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                hora = estatusBandeja.horaLlenada?.ToString("hhmmss", System.Globalization.CultureInfo.InvariantCulture),
                usuarioLlenada = estatusBandeja.usuarioLlenada,
                secSalida = estatusBandeja.secuenciaSalida,
                batchId = estatusBandeja.batchId,
                menge = estatusBandeja.cantidad,
                meins = estatusBandeja.UMP,
                cpudt = estatusBandeja.fechaRegistro.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                cputm = estatusBandeja.horaRegistro.ToString("hhmmss", System.Globalization.CultureInfo.InvariantCulture),
                fechaVaciada = estatusBandeja.fechaVaciada?.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                horaVaciada = estatusBandeja.horaVaciada?.ToString("hhmmss", System.Globalization.CultureInfo.InvariantCulture),
                idEquipoVaciado = estatusBandeja.idEquipoVaciado,
                usuarioVaciada = estatusBandeja.usuarioVaciada,
                formaVaciada = estatusBandeja.formaVaciada
            };
        }
        /// <summary>
        /// Metodo que determina si la bandeja está disponible para poder cargar materiales en ella
        /// </summary>
        /// <param name="idBandeja"></param>
        /// <param name="secuenciaBandeja"></param>
        public static void ValidacionEstatusBandeja(string idBandeja, int secuenciaBandeja, string status)
        {
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    var estatusBandeja = ctx.EstatusBandejas.SingleOrDefault<EstatusBandeja>(
                        x => x.idBandeja == idBandeja && x.secuenciaBandeja == secuenciaBandeja);
                    if (estatusBandeja != null)
                    {
                        if (estatusBandeja.estatus == "LL" && status == "LL")
                        {
                            var dateTime = new DateTime((long)estatusBandeja.horaLlenada?.Ticks);
                            var formattedHoraLlenada = dateTime.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                            throw new Exception($"Bandeja {estatusBandeja.idBandeja + estatusBandeja.secuenciaBandeja.ToString("00")} llena con Producto: {estatusBandeja.idMaterial.TrimStart(new Char[] { '0' })}, de la Máquina: {estatusBandeja.idEquipoLlenado}, " + Environment.NewLine + $"Fecha: {estatusBandeja.fechaLlenada?.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)}, Hora: {formattedHoraLlenada}");
                        }
                        else if (estatusBandeja.estatus == "VA" && status == "VA")
                        {
                            //throw new Exception($"La bandeja {estatusBandeja.idBandeja + estatusBandeja.secuenciaBandeja.ToString("00")} ya ha sido registrada por el equipo: {estatusBandeja.idEquipoVaciado} ");
                            throw new Exception($"Bandeja {estatusBandeja.idBandeja + estatusBandeja.secuenciaBandeja.ToString("00")} sin producto");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Metodo para determinar si una bandeja ya ha sido registrada
        /// </summary>
        /// <param name="idBandeja"></param>
        /// <param name="secuenciaBandeja"></param>
        /// <returns></returns>
        public static EstatusBandeja GetBandejaRegistrada(string idBandeja, int secuenciaBandeja)
        {
            EstatusBandeja registrada = null;
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    var estatusBandeja = ctx.EstatusBandejas.SingleOrDefault<EstatusBandeja>(
                        x => x.idBandeja == idBandeja && x.secuenciaBandeja == secuenciaBandeja);
                    registrada = estatusBandeja;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return registrada;
        }
    }
}