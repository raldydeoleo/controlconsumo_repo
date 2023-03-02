using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Shared.Tables;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo
{
    public class RepositoryFactory
    {
        public MyDbConnection Connection { get; private set; }
        public RepositoryFactory(MyDbConnection connection)
        {
            this.Connection = connection;
        }
        public RepositoryZ GetRepositoryZ()
        {
            return new RepositoryZ(this.Connection);
        }
        public RepositoryR GetRepositoryR()
        {
            return new RepositoryR(this.Connection);
        }
        public IRepository<Settings> GetRepositorySettings()
        {
            return new RepositorySettings(this.Connection);
        }
        public IRepository<Users> GetRepositoryUsers()
        {
            return new RepositoryUsers(this.Connection);
        }
        public IRepository<Rols> GetRepositoryRols()
        {
            return new RepositoryRols(this.Connection);
        }
        public IRepository<RolsPermits> GetRepositoryRolsPermits()
        {
            return new RepositoryRolsPermits(this.Connection);
        }
        public IRepository<Materials> GetRepositoryMaterials()
        {
            return new RepositoryMaterials(this.Connection);
        }
        public IRepository<Lots> GetRepositoryLots()
        {
            return new RepositoryLots(this.Connection);
        }
        public IRepository<Equipments> GetRepositoryEquipments()
        {
            return new RepositoryEquipments(this.Connection);
        }
        public IRepository<Configs> GetRepositoryConfigs()
        {
            return new RepositoryConfigs(this.Connection);
        }
        public IRepository<Areas> GetRepositoryAreas()
        {
            return new RepositoryAreas(this.Connection);
        }
        public IRepository<AreasEquipments> GetRepositoryAreasEquipments()
        {
            return new RepositoryAreasEquipments(this.Connection);
        }
        public IRepository<ConfigMaterials> GetRepositoryConfigMaterials()
        {
            return new RepositoryConfigsMaterials(this.Connection);
        }
        public IRepository<EquipmentTypes> GetRepositoryEquipmentTypes()
        {
            return new RepositoryEquipmentTypes(this.Connection);
        }
        public IRepository<Times> GetRepositoryTimes()
        {
            return new RepositoryTimes(this.Connection);
        }
        public IRepository<Turns> GetRepositoryTurns()
        {
            return new RepositoryTurns(this.Connection);
        }
        public IRepository<Groups> GetRepositoryGroups()
        {
            return new RepositoryGroups(this.Connection);
        }
        public IRepository<Stocks> GetRepositoryStocks()
        {
            return new RepositoryStocks(this.Connection);
        }
        public IRepository<StocksDetails> GetRepositoryStocksDetails()
        {
            return new RepositoryStocksDetails(this.Connection);
        }
        public IRepository<Elaborates> GetRepositoryElaborates()
        {
            return new RepositoryElaborates(this.Connection);
        }
        public IRepository<Consumptions> GetRepositoryConsumptions()
        {
            return new RepositoryConsumptions(this.Connection);
        }
        public IRepository<MaterialsZilm> GetRepositoryMaterialZilm()
        {
            return new RepositoryMaterialZilms(this.Connection);
        }
        public IRepository<CustomSecuences> GetRepositoryCustomSecuences()
        {
            return new RepositoryCustomSecuences(this.Connection);
        }
        public IRepository<Syncro> GetRepositorySyncro()
        {
            return new RepositorySyncro(this.Connection);
        }
        public IRepository<Trays> GetRepositoryTrays()
        {
            return new RepositoryTrays(this.Connection);
        }
        public IRepository<TraysProducts> GetRepositoryTraysProducts()
        {
            return new RepositoryTraysProducts(this.Connection);
        }
        public IRepository<TraysTimes> GetRepositoryTraysTimes()
        {
            return new RepositoryTraysTimes(this.Connection);
        }
        public IRepository<Tracking> GetRepositoryTracking()
        {
            return new RepositoryTracking(this.Connection);
        }
        public IRepository<Errors> GetRepositoryErrors()
        {
            return new RepositoryErrors(this.Connection);
        }
        public IRepository<TraysRelease> GetRepositoryTraysRelease()
        {
            return new RepositoryTraysRelease(this.Connection);
        }
        public IRepository<TraysReleasePosition> GetRepositoryTraysReleasePosition()
        {
            return new RepositoryTraysReleasePosition(this.Connection);
        }
        public IRepository<Units> GetRepositoryUnits()
        {
            return new RepositoryUnits(this.Connection);
        }
        public IRepository<Wastes> GetRepositoryWastes()
        {
            return new RepositoryWastes(this.Connection);
        }
        public IRepository<WastesMaterials> GetRepositoryWasteMaterials()
        {
            return new RepositoryWastesMaterials(this.Connection);
        }
        public IRepository<MaterialsProcess> GetRepositoryMaterialsProcess()
        {
            return new RepositoryMaterialsProcess(this.Connection);
        }       
        public IRepository<Inventories> GetRepositoryInventories()
        {
            return new RepositoryInventories(this.Connection);
        }
        public IRepository<Transactions> GetRepositoryTransactions()
        {
            return new RepositoryTransactions(this.Connection);
        }
        public IRepository<ProductsRoutes> GetRepositoryProductsRoutes()
        {
            return new RepositoryProductsRoutes(this.Connection);
        }
        public IRepository<ConfiguracionTiempoSalida> GetRepositoryConfiguracionTiempoSalidas()
        {
            return new RepositoryConfiguracionTiempoSalidas(this.Connection);
        }
        public IRepository<TipoProductoTerminado> GetRepositoryTipoProductoTerminados()
        {
            return new RepositoryTipoProductoTerminados(this.Connection);
        }
        public IRepository<TipoAlmacenamientoProducto> GetRepositoryTipoAlmacenamientoProductos()
        {
            return new RepositoryTipoAlmacenamientoProductos(this.Connection);
        }
        public IRepository<ProductoTipoAlmacenamiento> GetRepositoryProductoTipoAlmacenamientos()
        {
            return new RepositoryProductoTipoAlmacenamientos(this.Connection);
        }
        public IRepository<ConfiguracionSincronizacionTablas> GetRepositoryConfiguracionSincronizacionTablas()
        {
            return new RepositoryConfiguracionSincronizacionTablas(this.Connection);
        }
        public IRepository<ConfiguracionInicialControlSalidas> GetRepositoryConfiguracionInicialControlSalidas()
        {
            return new RepositoryConfiguracionInicialControlSalidas(this.Connection);
        }
        public IRepository<LabelPrintingLogs> GetRepositoryLabelPrintingLogs()
        {
            return new RepositoryLabelPrintingLogs(this.Connection);
        }
        public IRepository<LabelPrintingReason> GetRepositoryLabelPrintingReasons()
        {
            return new RepositoryLabelPrintingReasons(this.Connection);
        }
        public RepositoryDataBase GetRepositoryDataBase()
        {
            return new RepositoryDataBase(this.Connection);
        }
    }
}
