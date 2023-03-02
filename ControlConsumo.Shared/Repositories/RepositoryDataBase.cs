using System;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Async;
using ControlConsumo.Shared.Tables;

namespace ControlConsumo.Shared.Repositories
{
    /// <summary>
    /// Modelo para la base de datos.
    /// Este modelo no hereda de la interface IModel, porque solo
    /// se utiliza para crear la BD y controlar los cambios.
    /// Aristoteles Estrella Garcia 13.01.15
    /// </summary>
    public class RepositoryDataBase : RepositoryBase
    {
        public RepositoryDataBase(SQLiteAsyncConnection Connection) : base(Connection) { }

        public RepositoryDataBase(MyDbConnection Connection) : base(Connection) { }

        /// <summary>
        /// Metodo para crear la Base de Datos en Caso de no Existir.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        public Boolean CreateDb()
        {
            try
            {
                DataBaseLocked = true;

                var con = GetConnection();

                #region Creacion de las Tablas

                con.CreateTable<ConfiguracionSincronizacionTablas>();
                con.CreateTable<Settings>();
                con.CreateTable<Syncro>();
                con.CreateTable<Rols>();
                con.CreateTable<Users>();
                con.CreateTable<RolsPermits>();
                con.CreateTable<Areas>();
                con.CreateTable<AreasEquipments>();
                con.CreateTable<Configs>();
                con.CreateTable<Equipments>();
                con.CreateTable<EquipmentTypes>();
                con.CreateTable<Lots>();
                con.CreateTable<Materials>();
                con.CreateTable<Areas>();
                con.CreateTable<Turns>();
                con.CreateTable<Units>();
                con.CreateTable<ConfigMaterials>();
                con.CreateTable<EquipmentTypes>();
                con.CreateTable<Times>();
                con.CreateTable<Turns>();
                con.CreateTable<Consumptions>();
                con.CreateTable<Elaborates>();
                con.CreateTable<Groups>();
                con.CreateTable<Stocks>();
                con.CreateTable<StocksDetails>();
                con.CreateTable<MaterialsZilm>();
                con.CreateTable<CustomSecuences>();
                con.CreateTable<Trays>();
                con.CreateTable<TraysTimes>();
                con.CreateTable<TraysProducts>();
                con.CreateTable<Tracking>();
                con.CreateTable<Errors>();
                con.CreateTable<TraysRelease>();
                con.CreateTable<TraysReleasePosition>();
                con.CreateTable<Wastes>();
                con.CreateTable<WastesMaterials>();
                con.CreateTable<MaterialsProcess>();
                con.CreateTable<Inventories>();
                con.CreateTable<Transactions>();
                con.CreateTable<ProductsRoutes>();
                con.CreateTable<Categories>();
                con.CreateTable<ConfiguracionTiempoSalida>();
                con.CreateTable<TipoProductoTerminado>();
                con.CreateTable<TipoAlmacenamientoProducto>();
                con.CreateTable<ProductoTipoAlmacenamiento>();
                con.CreateTable<ConfiguracionInicialControlSalidas>();
                con.CreateTable<LabelPrintingReason>();
                con.CreateTable<LabelPrintingLogs>();
                #endregion

                #region Creacion de los Indexes

                con.CreateIndex("Configs", new String[] { "EquipmentID", "Begin" }, false);
                con.CreateIndex("Configs", new String[] { "Status", "Begin" }, false);
                con.CreateIndex("ConfigMaterials", new String[] { "ProductCode", "VerID" }, false);
                con.CreateIndex("Wastes", new String[] { "MaterialCode", "StockID", "CustomFecha" }, false);
                con.CreateIndex("Consumptions", new String[] { "CustomFecha", "TurnID", "Produccion" }, false);
                con.CreateIndex("TraysProducts", new String[] { "TrayID", "Secuencia" }, false);
                con.CreateIndex("ProductsRoutes", new String[] { "ElaborateID", "Produccion", "EquipmentID" }, false);
                con.CreateIndex("ProductsRoutes", new String[] { "EquipmentID", "ElaborateID", "TrayID", "Produccion" }, false);
                con.CreateIndex("Transactions", new String[] { "MaterialCode", "Lot", "CustomFecha", "TurnID" }, false);
                con.CreateIndex("Inventories", new String[] { "MaterialCode", "Lot", "Quantity" }, false);
                con.CreateIndex("Lots", new String[] { "MaterialCode", "Reference" }, false);

                #endregion
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
            return true;
        }

        /// <summary>
        /// Metodo para actualizar la Base de Datos en Caso de Cambios durante el desarrollo.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        public Boolean CheckDb()
        {
            return true;
        }
    }
}
