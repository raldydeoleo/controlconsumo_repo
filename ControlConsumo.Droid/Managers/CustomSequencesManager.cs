using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlConsumo.Droid.Managers
{
    class CustomSequencesManager
    {
        private ProcessList _Process;
        public readonly List<CustomSecuences> Secuencias;
        private readonly IRepository<CustomSecuences> repo;
        private readonly IRepository<Tracking> TrackingRepo;
        public readonly CachingManager caching;
        private readonly RepositoryZ repoz;
        private DateTime Fecha { get; set; }
        private ActualConfig actualconfig { get; set; }
        public Times Time { get; private set; }

        public CustomSequencesManager()
        {
            Secuencias = new List<CustomSecuences>();
            repo = new RepositoryFactory(Util.GetConnection()).GetRepositoryCustomSecuences();
            TrackingRepo = new RepositoryFactory(Util.GetConnection()).GetRepositoryTracking();
            caching = new CachingManager(null);
            repoz = new RepositoryZ(Util.GetConnection());
            LoadSequences();
        }

        public void ReloadSequences()
        {
            Secuencias.Clear();
            LoadSequences();
        }

        private async void LoadSequences()
        {
            var temp = await repo.GetAsyncAll();
            Secuencias.AddRange(temp);
        }

        public async void CleanSequences()
        {
            var repoSetting = new RepositoryFactory(Util.GetConnection()).GetRepositorySettings();
            var repoz = new RepositoryZ(Util.GetConnection());
            var proceso = await repoz.GetProces();
            var ActualConfig = await repoz.GetActualConfig(proceso.EquipmentID);
            if (ActualConfig != null)
            {
                var _Materials = await repoz.GetMaterialConfigAsync(ActualConfig.ProductCode, ActualConfig.VerID);
                Secuencias.RemoveAll(m => !_Materials.Select(p => p.MaterialCode).Contains(m.MaterialCode));
            }

            await repo.DeleteAllAsync(null);

            //var clone = Secuencias.Clone();

            //foreach (var item in clone)
            //{
            //    var result = await repoz.ExisteEnlos2UltimosTurnos(item.MaterialCode, item.ConsumptionID, item.CustomFecha);
            //    if (!result) Secuencias.RemoveAll(a => a.MaterialCode == item.MaterialCode && a.ConsumptionID == item.ConsumptionID && a.CustomFecha == item.CustomFecha);
            //}

            await Save(Secuencias);

            await repoSetting.InsertOrReplaceAsync(new Settings()
            {
                Key = Settings.Params.AllComponentAreInside,
                Value = "false"
            });
        }

        /// <summary>
        /// Método para generar las secuencias por cada entrada
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <param name="Lot"></param>
        /// <param name="IsLast"></param>
        /// <returns></returns>
        public async Task<Int16> AddMaterial(String MaterialCode, String Lot, Boolean IsLast = false)
        {
            if (_Process == null)
            {
                _Process = await repoz.GetProces();
            }

            var LastRegister = Secuencias.SingleOrDefault(p => p.MaterialCode == MaterialCode && p._Fecha.GetSapDate() == caching.GetProductionDate().GetSapDate());

            Int16 max = 0;

            if (LastRegister == null)
            {
                LastRegister = Secuencias.FirstOrDefault(p => p.MaterialCode == MaterialCode);

                if (LastRegister != null)
                {
                    if (IsLast)
                    {
                        var tracking = await repoz.GetTrackingWithCount(LastRegister._Fecha, LastRegister.ConsumptionID);

                        if (tracking.WasNull && tracking.Count > 0)
                        {
                            await TrackingRepo.InsertAsync(new Tracking()
                            {
                                ConsumptionID = LastRegister.ConsumptionID,
                                ElaborateID = 1,
                                FechaConsumption = LastRegister._Fecha,
                                FechaElaborate = caching.GetProductionDate(),
                                Sync = true,
                                SyncSQL = true
                            });
                        }
                    }
                }

                Secuencias.RemoveAll(p => p.MaterialCode == MaterialCode);

                max = await GetNextSequence();

                var sec = new CustomSecuences()
                {
                    CustomFecha = Convert.ToInt32(caching.GetProductionDate().GetSapDate()),
                    HasChanged = true,
                    IsMemoryCreated = true,
                    Fecha = caching.GetProductionDate(),
                    Fecha2 = caching.GetProductionDate(),
                    ConsumptionID = max,
                    ElaborateID = 0,
                    MaterialCode = MaterialCode
                };

                Secuencias.Add(sec);
            }
            else
            {
                if (IsLast)
                {
                    var tracking = await repoz.GetTracking(LastRegister._Fecha, LastRegister.ConsumptionID);

                    if (tracking == null)
                    {
                        var sec = LastRegister.ElaborateID == 0 ? 1 : LastRegister.ElaborateID;

                        await TrackingRepo.InsertAsync(new Tracking()
                        {
                            ConsumptionID = LastRegister.ConsumptionID,
                            ElaborateID = (short)sec,
                            FechaConsumption = LastRegister._Fecha,
                            FechaElaborate = LastRegister._Fecha2,
                            Sync = true,
                            SyncSQL = true
                        });
                    }
                }

                max = await GetNextSequence();
                LastRegister.CustomFecha = Convert.ToInt32(caching.GetProductionDate().GetSapDate());
                LastRegister.ConsumptionID = max;
                LastRegister.HasChanged = true;
            }

            await Save(Secuencias);

            return max;
        }

        /// <summary>
        /// Método para agregar la secuencia usado por dos maquinas
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <param name="Lot"></param>
        /// <param name="Secuence"></param>
        /// <returns></returns>
        public async Task AddMaterial(String MaterialCode, String Lot, Int16 Secuence)
        {
            var sec = new CustomSecuences()
            {
                CustomFecha = Convert.ToInt32(caching.GetProductionDate().GetSapDate()),
                HasChanged = true,
                IsMemoryCreated = true,
                Fecha = caching.GetProductionDate(),
                Fecha2 = caching.GetProductionDate(),
                ConsumptionID = Secuence,
                ElaborateID = !Secuencias.Any() ? (short)1 : Secuencias.Max(m => m.ElaborateID),
                MaterialCode = MaterialCode
            };

            Secuencias.RemoveAll(r => r.MaterialCode == MaterialCode);

            Secuencias.Add(sec);

            await Save(Secuencias);
        }

        private async Task<Int16> GetNextSequence()
        {
            return await repoz.GetNextSequenceAsync(caching.GetProductionDate(), _Process);
        }

        private async Task<Int16> GetNextOut(DateTime? FechaCierre)
        {
            try
            {
                return await repoz.GetNextOutAsync(FechaCierre ?? caching.GetProductionDate());
            }
            catch(Exception)
            {
                throw;
            }
        }

        private async Task Save(List<CustomSecuences> sequences)
        {
            try
            {
                await repo.InsertOrReplaceAsyncAll(sequences.Clone());
            }
            catch(Exception ex)
            {
                await Util.SaveException(ex, "Guardado de secuencias.");
            }
        }

        private async Task SaveTracking()
        {
            var buffer = Secuencias.Select(p => new Tracking()
            {
                Sync = true,
                SyncSQL = true,
                ConsumptionID = p.ConsumptionID,
                ElaborateID = p.ElaborateID,
                FechaConsumption = p.Fecha,
                FechaElaborate = p._Fecha2
            }).ToList();

            await TrackingRepo.InsertAsyncAll(buffer.Clone());
        }

        public async Task<Int16> AddSalida(DateTime? FechaCierre = null)
        {
            //var LastOne = Secuencias.FirstOrDefault(p => p._Fecha2.GetSapDate() == caching.GetProductionDate().GetSapDate());
            try
            {
                var NextOut = await GetNextOut(FechaCierre);

                //if (LastOne != null)
                //    NextOut = LastOne.ElaborateID;

                //if (Secuencias.Any(p => p.HasChanged) || LastOne == null || NextOut == 0)
                //{
                //    NextOut = GetNextOut();               
                //}
                for (int i = 0; i < Secuencias.Count; i++)
                {
                    Secuencias[i].Fecha2 = caching.GetProductionDate();
                    Secuencias[i].ElaborateID = NextOut;
                    Secuencias[i].HasChanged = false;
                }

                await SaveTracking();

                await Save(Secuencias);

                return NextOut;
            }
            catch(Exception ex)
            {
                await Util.SaveException(ex, "Generación de Secuencia de Salida");
                throw;
            }
        }
    }
}