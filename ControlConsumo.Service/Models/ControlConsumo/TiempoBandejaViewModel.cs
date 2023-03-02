using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class TiempoBandejaViewModel
    {
        public int Id { get; set; }
        public string idProceso { get; set; }
        public string idTiempo { get; set; }
        public string idBandeja { get; set; }
        public double cantidad { get; set; }
        public string unidad { get; set; }
        public DateTime fechaRegistro { get; set; }

        public static implicit operator TiempoBandejaViewModel(TiempoBandeja tiempoBandeja)
        {
            var tiempoBandejaViewModel = new TiempoBandejaViewModel();
            tiempoBandejaViewModel.Id = tiempoBandeja.Id;
            tiempoBandejaViewModel.idProceso = tiempoBandeja.idProceso;
            tiempoBandejaViewModel.idBandeja = tiempoBandeja.idBandeja;
            tiempoBandejaViewModel.idTiempo = tiempoBandeja.idTiempo;
            tiempoBandejaViewModel.cantidad = tiempoBandeja.cantidad;
            tiempoBandejaViewModel.fechaRegistro = tiempoBandeja.fechaRegistro;
            tiempoBandejaViewModel.unidad = tiempoBandeja.unidad;
            return tiempoBandejaViewModel;
        }
    }
}