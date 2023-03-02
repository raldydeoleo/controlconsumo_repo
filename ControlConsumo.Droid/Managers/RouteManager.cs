using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Repositories;
using System.Threading.Tasks;
using ControlConsumo.Shared.Models.Z;

namespace ControlConsumo.Droid.Managers
{
    class RouteManager
    {
        public ProductsRoutes Ruta { get; private set; }
        private readonly IRepository<ProductsRoutes> repo;
        private readonly IRepository<Elaborates> repoSalida;
        private readonly RepositoryZ repoz;
        private TraysList Bandeja { get; set; }
        public Boolean IsThereConnection { get; set; }

        public delegate void SetPackID(String PackID);
        public event SetPackID OnPackID;

        public RouteManager()
        {
            repo = new RepositoryFactory(Util.GetConnection()).GetRepositoryProductsRoutes();
            repoz = new RepositoryZ(Util.GetConnection());
        }

        public async void SetTraza(ProductsRoutes _Ruta)
        {
            Ruta = await repoz.LoadRoutebySync(_Ruta.EquipmentID, _Ruta.ElaborateID, _Ruta.TrayID);
            Ruta.IsActive = true;

            if (Ruta != null)
            {
                await repo.UpdateAsync(Ruta);
            }
            else
            {
                await repo.InsertAsync(Ruta);
            }

            await repoz.CleanTraza();
        }

        public void SetBandeja(TraysList Bandeja)
        {
            this.Bandeja = Bandeja;
            //if (OnSetBachID != null && Bandeja != null)
            //{
            //    OnSetBachID.Invoke(Bandeja.BatchID, String.Empty);
            //}
        }

        public async void Reload(String Equipment)
        {
            var reposetting = new RepositoryFactory(Util.GetConnection()).GetRepositorySettings();
            var lastBandeja = await repoz.GetLastBandejaEntrada();

            if (lastBandeja != null)
            {
                Ruta = await repoz.LoadRoutebySync(lastBandeja.TrayEquipmentID, lastBandeja.ElaborateID, lastBandeja.TrayID);

                if (Ruta != null)
                {
                    Ruta.IsActive = true;
                    await repo.UpdateAsync(Ruta);
                    await reposetting.InsertOrReplaceAsync(new Settings()
                    {
                        Key = Settings.Params.BatchID,
                        Value = Ruta.BatchID
                    });
                }
            }

            await repoz.CleanTraza();

            //var LastRoute = await repoz.LoadLastRoute(Equipment);

            //if (LastRoute != null && !String.IsNullOrEmpty(LastRoute.TimeID2))
            //{
            //    Ruta = await repoz.LoadRouteByRealKey(LastRoute.TimeID2, LastRoute.Year2, LastRoute.CustomID2);

            //    if (Ruta != null)
            //    {
            //        Ruta.IsActive = true;
            //        await repo.UpdateAsync(Ruta);
            //        await reposetting.InsertOrReplaceAsync(new Settings()
            //        {
            //            Key = Settings.Params.BatchID,
            //            Value = Ruta.BatchID
            //        });
            //    }
            //}
        }

        public async void Clean()
        {
            if (Ruta != null)
            {
                Ruta.Sync = true;
                Ruta.Status = ProductsRoutes.RoutesStatus.Procesado;
                Ruta.IsActive = false;
                Ruta.End = DateTime.Now;
                Ruta.Fecha = DateTime.Now;
                await repo.UpdateAsync(Ruta);
                Ruta = null;
            }
        }

        public async void Load()
        {
            Ruta = await repoz.LoadActive();
            var salida = await repoz.GetLastSalidaAsync();
            if (salida != null && OnPackID != null)
            {
                OnPackID.Invoke(salida.PackID);
            }
        }

        //public async void LoadRoutebySync(DateTime FechaConsumo)
        //{
        //    if (Bandeja.Fecha.HasValue)
        //    {
        //        var route = await repoz.LoadRoutebySync(Bandeja.EquipmentID, Bandeja.ElaborateID, Bandeja.TrayID);

        //        if (route != null)
        //        {
        //            Bandeja.Fecha = route.Produccion;
        //            LoadRoute(FechaConsumo);
        //        }
        //    }
        //}

        public async void LoadRoute(DateTime Produccion)
        {
            var NewRoute = await repoz.LoadRoutebySync(Bandeja.EquipmentID, Bandeja.ElaborateID, Bandeja._TrayID);

            var buffer = new List<ProductsRoutes>();

            if (NewRoute != null)
            {
                NewRoute.Status = ProductsRoutes.RoutesStatus.EnEquipo;
                NewRoute.Sync = true;
                NewRoute.IsActive = true;
                NewRoute.Fecha = DateTime.Now;

                if (NewRoute.Begin.Year == 1) NewRoute.Begin = Produccion;

                buffer.Add(NewRoute);
            }

            if (Ruta != null)
            {
                Ruta.Sync = true;
                Ruta.Status = ProductsRoutes.RoutesStatus.Procesado;
                Ruta.IsActive = false;
                Ruta.End = Produccion;
                Ruta.Fecha = DateTime.Now;
                buffer.Add(Ruta);
            }

            if (buffer.Any()) await repo.UpdateAllAsync(buffer);

            Ruta = NewRoute;

            if (Ruta != null && OnPackID != null)
            {
                OnPackID.Invoke(Ruta.PackID);
            }
        }

        public async Task<ProductsRoutes> WriteOutput(Elaborates Salida)
        {
            var track = new ProductsRoutes()
            {
                Status = ProductsRoutes.RoutesStatus.EnTransito,
                ProcessID = Salida.ProcessID,
                TimeID = Salida.TimeID,
                Year = DateTime.Now.Year.ToString(),
                CustomID = 0,
                EquipmentID = Salida.EquipmentID,
                Center = Salida.Center,
                ProductCode = Salida.ProductCode,
                VerID = Salida.VerID,
                Lot = Salida.Lot,
                CustomFecha = Convert.ToInt32(Salida.Produccion.GetSapDate()),
                Produccion = Salida.Produccion,
                TurnID = Salida.TurnID,
                TrayID = Salida.TrayID,
                BatchID = Salida.BatchID,
                Quantity = Salida.Quantity,
                Peso = Salida.Peso,
                Unit = Salida.Unit,
                Sync = true,
                Logon = Salida.Logon,
                ElaborateID = Convert.ToInt16(Salida.CustomID),
                Fecha = Salida.Fecha,
                PackID = Salida.PackID,
                TimeID2 = String.Empty,
                Year2 = String.Empty,
                SecuenciaEmpaque = Convert.ToInt16(Salida.PackSequence)
            };

            var lotemanual = String.Empty;

            if (String.IsNullOrEmpty(Salida.SubEquipmentID))
            {
                lotemanual = Salida.EquipmentID;
            }
            else
            {
                lotemanual = String.Format("{0}-{1}", Salida.EquipmentID, Salida.SubEquipmentID);
            }

            if (Ruta != null)
            {
                track.LotManufacture = String.Format("{0}-{1}", Ruta.LotManufacture, lotemanual);
                track.CustomID2 = Ruta.CustomID;
                track.TimeID2 = Ruta.TimeID;
                track.Year2 = Ruta.Year;
                track.Fecha = DateTime.Now;
            }
            else
            {
                if (Bandeja == null)
                {
                    var entrada = await repoz.GetLastBandejaEntrada();
                    if (entrada != null)
                    {
                        Bandeja = new TraysList
                        {
                            EquipmentID = entrada.TrayEquipmentID,
                            BatchID = entrada.BatchID,
                        };
                    }
                }

                if (Bandeja != null)
                {
                    if (!String.IsNullOrEmpty(Bandeja.BatchID))
                        track.LotManufacture = String.Format("{0}-{1}-{2}", Bandeja.BatchID.Substring(9, 3), Bandeja.EquipmentID, lotemanual);
                    else
                        track.LotManufacture = String.Format("{0}-{1}", Bandeja.EquipmentID, lotemanual);
                }
            }

            await repo.InsertAsync(track);

            if (track != null && OnPackID != null) OnPackID.Invoke(track.PackID);

            return track;
        }

        public async Task<String> GetBatchID(String EquipmentID, Byte TurnID, DateTime Fecha, String TimeID)
        {
            try
            {
                if (!TimeID.PadLeft(2, '0').Equals("01"))
                {
                    var setting = await repoz.GetSettingAsync<String>(Settings.Params.BatchID, String.Empty);
                    return setting;
                }
                else
                {
                    return String.Format("{0}{1}{2}", Fecha.ToString("ddMMyyyy"), TurnID, EquipmentID);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}