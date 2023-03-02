using ControlConsumo.Service.Models.ControlConsumo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class BandejaViewModel
    {
        public string idBandeja { get; set; }
        public int secuenciaInicial { get; set; }
        public int secuenciaFinal { get; set; }
        public bool procesarSap { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public bool estatusVigencia { get; set; }

        public static implicit operator BandejaViewModel(Bandeja bandeja)
        {
            var bandejaViewModel = new BandejaViewModel();
            bandejaViewModel.idBandeja = bandeja.idBandeja;
            bandejaViewModel.secuenciaInicial = bandeja.secuenciaInicial;
            bandejaViewModel.secuenciaFinal = bandeja.secuenciaFinal;
            bandejaViewModel.procesarSap = bandeja.procesarSap;
            bandejaViewModel.fechaRegistro = bandeja.fechaRegistro;
            bandejaViewModel.usuarioRegistro = bandeja.usuarioRegistro;
            bandejaViewModel.estatusVigencia = bandeja.estatusVigencia;
            return bandejaViewModel;
        }
    }
}