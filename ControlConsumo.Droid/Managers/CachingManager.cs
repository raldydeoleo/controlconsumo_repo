using System;
using System.Collections.Generic;
using System.Linq;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Shared.Models.Material;
using ControlConsumo.Shared.Models.Lot;
using ControlConsumo.Shared.Models.Config;
using System.Threading.Tasks;
using ControlConsumo.Droid.Activities.Bundles;
using Android.Content;

namespace ControlConsumo.Droid.Managers
{
    public class CachingManager
    {
        public delegate void Loaded(Byte TurnoID, Boolean EquipmentSynced);
        public event Loaded OnLoaded;

        private readonly Context context;
        private readonly RepositoryFactory repo = new RepositoryFactory(Util.GetConnection());
        public static Int16 MaxMinutes { get; private set; }
        public Byte Copies { get; private set; }
        public Byte TurnoID { get { return GetTurn() ?? 0; } }
        public Stocks Stock { get; private set; }
        public String ProductCode { get; private set; }
        public String VerID { get; private set; }
        public IEnumerable<Turns> turnos { get; private set; }
        public IEnumerable<Trays> bandejas { get; private set; }
        private IEnumerable<MaterialList> _Materials { get; set; }
        private IEnumerable<ConfiguracionTiempoSalida> _ConfiguracionesTiempoSalida { get; set; }
        private IEnumerable<ConfiguracionTiempoConsumo> _ConfiguracionesTiempoConsumo { get; set; }//agregado por Raldy para funcionalidad tiempo consumo en PMB 15-02-2023
        private IEnumerable<LotsList> _Batches { get; set; }
        public IEnumerable<MaterialList> Materials
        {
            get
            {
                try
                {
                    if (_Materials == null || !_Materials.Any())
                    {
                        var repoz = repo.GetRepositoryZ();
                        _Materials = repoz.GetMaterialConfig(ProductCode, VerID);
                    }
                }
                catch (NullReferenceException)
                {
                    return new List<MaterialList>();
                }
                catch (Exception)
                {
                    throw;
                }

                return _Materials;
            }
        }

        public List<ConfiguracionTiempoSalida> ConfiguracionesTiempoSalida
        {
            get
            {
                if (_ConfiguracionesTiempoSalida == null)
                {
                    var repoz = repo.GetRepositoryZ();
                    _ConfiguracionesTiempoSalida = repoz.CargarConfiguracionTiemposSalidas();
                }

                return _ConfiguracionesTiempoSalida.ToList();
            }
        }

        //CREADO POR RALDY PARA TIEMPO CONSUMO EN PMB 15-02-2023
        public List<ConfiguracionTiempoConsumo> ConfiguracionesTiempoConsumo
        {
            get
            {
                if (_ConfiguracionesTiempoConsumo == null)
                {
                    var repoz = repo.GetRepositoryZ();
                    _ConfiguracionesTiempoConsumo = repoz.CargarConfiguracionTiemposConsumos();
                }

                return _ConfiguracionesTiempoConsumo.ToList();
            }
        }

        public IEnumerable<LotsList> Batches
        {
            get
            {
                try
                {
                    if (_Batches == null || _Batches.Any())
                    {
                        var repoz = repo.GetRepositoryZ();
                        _Batches = repoz.GetMaterialLotConfig(Materials.Select(p => p.MaterialCode).ToList());
                    }
                }
                catch (NullReferenceException)
                {
                    return new List<LotsList>();
                }
                catch (Exception)
                {
                    throw;
                }

                return _Batches;
            }
        }
        public IEnumerable<NextConfig> Configs { get; private set; }
        public Boolean IsInitialized
        {
            get
            {
                if (_Materials == null || Batches == null)
                    return false;
                else if (!Materials.Any() && !Batches.Any())
                    return false;
                else
                    return true;
            }
        }

        public CachingManager(Context context)
        {
            this.context = context;
            if (turnos == null)
            {
                Init();
            }
        }

        public void SetBundle(MenuBundles Bundle)
        {
            ProductCode = Bundle.ProductCode;
            VerID = Bundle.VerID;
            MaxMinutes = Bundle.MaxMinutes;
            Copies = Bundle.Copies;
            turnos = Bundle.turnos;
            Stock = Bundle.Stock;
            bandejas = Bundle.bandejas;
            Configs = Bundle.Configs;
            _Materials = Bundle.Materials;
            _Batches = Bundle.Batches;

            if (!IsInitialized)
                LoadProductConfig(ProductCode, VerID);

            if (OnLoaded != null)
                OnLoaded.Invoke(TurnoID, Stock != null ? true : false);
        }

        private async void Init()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var repoTrays = repo.GetRepositoryTrays();
                var repoMat = repo.GetRepositoryMaterials();
                var repoUnit = repo.GetRepositoryUnits();
                var reposs = repo.GetRepositorySettings();

                var HasEquipment = await repoz.GetSettingAsync<Boolean>(Settings.Params.EquipmentSynced, false);

                if (HasEquipment)
                {
                    if (context != null)
                    {
                        var arregloTurno = context.Resources.GetStringArray(Resource.Array.MinutosTurno);
                        var MaxMinutes = arregloTurno.Max(p => Convert.ToByte(p));
                        SetMaxMinutes(MaxMinutes);
                    }
                    await LoadStock();
                }

                bandejas = await repoTrays.GetAsyncAll();
                if (OnLoaded != null) OnLoaded.Invoke(TurnoID, HasEquipment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime GetProductionDate(Boolean AddMinutes = false)
        {
            VolveraCalcular:

            try
            {
                var repoz = repo.GetRepositoryZ();

                if (turnos == null)
                {
                    turnos = repoz.GetAllTurns();
                }

                var minHora = turnos.Min(p => p.End.ToLocalTime()).Hour * -1;

                var fecha = DateTime.Now.AddHours(minHora);

                if (AddMinutes) fecha.AddMinutes(MaxMinutes);

                if (Stock == null || (Stock != null && Stock.TurnID != TurnoID))
                {
                    var stock = repoz.ExistClosedStock(fecha, TurnoID);

                    if (stock != null && stock.Status == Stocks._Status.Cerrado)
                    {
                        fecha = fecha.AddMinutes(MaxMinutes);
                    }
                }

                return new DateTime(fecha.Year, fecha.Month, fecha.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }
            catch (NullReferenceException)
            {
                goto VolveraCalcular;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Byte GetNextTurn()
        {
            var fecha = DateTime.Now;
            var repoz = repo.GetRepositoryZ();
            var stock = repoz.ExistClosedStock(fecha, TurnoID);

            if (Stock == null || (Stock != null && Stock.TurnID != TurnoID))
            {
                fecha = fecha.AddMinutes(MaxMinutes);
            }

            return GetTurn(fecha).Value;
        }

        private Byte? GetTurn(DateTime? fecha = null)
        {
            if (turnos == null)
            {
                var repot = repo.GetRepositoryZ();
                turnos = repot.GetAllTurns();
            }

            Int16 hora = 0;

            if (!fecha.HasValue)
                hora = Convert.ToInt16(DateTime.Now.ToString("HH"));
            else
                hora = Convert.ToInt16(fecha.Value.ToString("HH"));

            var turno = turnos.SingleOrDefault(p => p.BeginH <= hora && p.EndH > hora);

            if (turno == null)
            {
                turno = turnos.LastOrDefault();
            }

            if (turno != null)
                return turno.ID;
            else
                return null;
        }

        public Boolean FinishTheTurn()
        {
            var turno = GetTurn();

            if (!turno.HasValue) return false;

            if (turno.Value != Stock.TurnID) return true;

            return false;
        }

        public Byte TurnHour()
        {
            var primer = turnos.First();
            return (Byte)(primer.EndH - primer.BeginH);
        }

        public void SetMaxMinutes(Int16 maxMinutes)
        {
            MaxMinutes = maxMinutes;
        }

        public void SetProduct(String ProductCode, String VerID)
        {
            this.ProductCode = ProductCode;
            this.VerID = VerID;
            LoadProductConfig(ProductCode, VerID);
        }

        private async void LoadProductConfig(String ProductCode, String VerID)
        {
            var repoz = repo.GetRepositoryZ();
            _Materials = await repoz.GetMaterialConfigAsync(ProductCode, VerID);
            _Batches = await repoz.GetMaterialLotConfigAsync(Materials.Select(p => p.MaterialCode).ToList());
            _ConfiguracionesTiempoSalida = repoz.CargarConfiguracionTiemposSalidas();
        }

        public async Task LoadNextConfigs(String EquipmentID)
        {
            var repoz = repo.GetRepositoryZ();
            Configs = await repoz.GetNextConfigs(EquipmentID);
        }

        private async Task LoadStock()
        {
            var repoz = repo.GetRepositoryZ();
            Stock = await repoz.GetActualStock();
        }

        public async Task ReloadStock()
        {
            Stock = null;
            await LoadStock();
        }

        public DateTime GetNextChangeDate()
        {
            var NextTurn = TurnoID + 1;
            Turns Turno = null;
            var fecha = DateTime.Now;

            try
            {
                Turno = turnos.First(p => p.ID == NextTurn);
                return new DateTime(fecha.Year, fecha.Month, fecha.Day, Turno.BeginH, 0, 0);
            }
            catch (Exception)
            {
                Turno = turnos.First();
                fecha = DateTime.Now.AddHours(Turno.BeginH);
                return new DateTime(fecha.Year, fecha.Month, fecha.Day, Turno.BeginH, 0, 0);
            }
        }

        public String GetPackID(String EquipmentID)
        {
            try
            {

                if (Stock == null) return String.Empty;
                var Turno = turnos.Single(p => p.ID == Stock.TurnID);
                var Fecha = GetProductionDate();
                var Hour = Turno.BeginH;
                Int32 counter = 0;

                counter++;

                while (true)
                {
                    if (Hour == Fecha.Hour)
                        break;

                    counter++;
                    Hour++;

                    if (Hour > 23) Hour = 0;
                }

                if ((Fecha.Hour == 0 ? 24 : Fecha.Hour) < Turno.BeginH && (Fecha.Hour > 21 || Stock.TurnID != 3 && counter > Turno.WorkHour))
                    counter = 1;
                else if (counter > Turno.WorkHour)
                    counter = Turno.WorkHour;

                return String.Format("{0}-{1}-{2}-{3}-{4}", Fecha.DayOfYear.ToString("000"), Turno.Empaque, Fecha.ToString("yy").Substring(1, 1), EquipmentID.Trim().Substring(2, 1), counter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Clean()
        {
            _Materials = null;
            _Batches = null;
        }
    }
}