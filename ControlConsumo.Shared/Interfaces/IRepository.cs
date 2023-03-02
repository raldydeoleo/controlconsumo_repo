using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Interfaces
{
    /// <summary>
    /// Interfax para los modelos.
    /// Aristoteles Estrella Garcia 13.01.15
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {       
        /// <summary>
        /// Metodo para obtener un valor por la llave asincrono
        /// </summary>
        /// <param name="key">Llave del objeto</param>
        /// <returns>Objeto</returns>
        Task<T> GetAsyncByKey(Object key);

        /// <summary>
        /// Metodo para cargar toda la tabla Asincrona
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAsyncAll();

        /// <summary>
        /// Metodo para Crear una instancia del Objeto
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <param name="model">Objeto a Crear</param>
        /// <returns>Retorno del ID del Objeto si su llave es un Campo numerico y autoincrementable</returns>
        Task<Boolean> InsertAsync(T model);

        /// <summary>
        /// Metodo para Insertar un Grupo de Registros.
        /// Aristoteles Estrella Garcia 17.01.15
        /// </summary>
        /// <param name="models">Registros</param>
        /// <returns>Retorno del Metodo</returns>
        Task<Boolean> InsertAsyncAll(IEnumerable<T> models);

        /// <summary>
        /// Metodo para Reemplazar el objeto si existe en la BD.
        /// </summary>
        /// <param name="model">Objeto</param>
        /// <returns>Confirmacion</returns>
        Task<Boolean> InsertOrReplaceAsync(T models);

        /// <summary>
        /// Metodo para Insertar o Reemplazar el Objeto en la DB.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<Boolean> InsertOrReplaceAsyncAll(IEnumerable<T> models);

        /// <summary>
        /// Elimina el Objeto en la base de datos.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <param name="model">Objeto a actualizar</param>
        /// <returns>Indica si el cambio marcho sin problemas</returns>
        Task<Boolean> DeleteAsync(T model);

        /// <summary>
        /// Metodo para Eliminar un Grupo de Registros.
        /// Aristoteles Estrella Garcia 17.01.15
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<Boolean> DeleteAllAsync(IEnumerable<T> models);

        /// <summary>
        /// Metodo para actualizar el registro en la base de datos.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <param name="model">Registro a actualizar</param>
        /// <returns>Confirmacion</returns>
        Task<Boolean> UpdateAsync(T model);

        /// <summary>
        /// Metedo para actualizar un grupo de registros
        /// Aristoteles Estrella Garcia 17.01.15
        /// </summary>
        /// <param name="models">Registros</param>
        /// <returns>Retorno</returns>
        Task<Boolean> UpdateAllAsync(IEnumerable<T> models);

        /// <summary>
        /// Metodo para sincronizar la tabla con el Servidor
        /// </summary>
        /// <returns>Retorno</returns>
        Task<Boolean> SyncAsync(Boolean processBySAP);

        /// <summary>
        /// Metodo para sincronizar en dos vias la tabla
        /// </summary>
        /// <returns></returns>
        Task<Boolean> SyncAsyncTwoWay();

        /// <summary>
        /// Metodo para realizar un carga inicial
        /// </summary>
        /// <returns>Retorno</returns>
        Task<Boolean> SyncAsyncAll(Boolean isItForInitialSync = true);

        /// <summary>
        /// Metodo para Crear la Base de Datos.
        /// </summary>
        /// <returns></returns>
        Task<Boolean> CreateAsync();

        /// <summary>
        /// Metodo para Borrar la Tabla de la DB
        /// </summary>
        /// <returns></returns>
        Task<Boolean> DropAsync();

        /// <summary>
        /// Metodo para Crear los Indices de la Tabla.
        /// </summary>
        /// <returns>Retorno</returns>
        Task<Boolean> CreateIndexAsync();

        /// <summary>
        /// Metodo para Validar Datos en Línea con SQL
        /// </summary>
        /// <param name="traysProducts"></param>
        /// <returns></returns>
        Task<String> InsertOrUpdateAsyncSql(T[] traysProducts, bool v=false);
    }
}
