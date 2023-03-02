using ControlConsumo.Service.Models.SodiQubeDB;
using ControlConsumo.Service.Models.ControlConsumo;
using ControlConsumo.Service.Tables;
using System;
using System.Linq;
using System.Data.SqlClient;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class ReportModel
    {

        public static ReportResult GetMaterialResult(String Equipo, String Material, String Lot, Single BoxNumber)
        {
            var retorno = new ReportResult();

             try
             {
                 using (var tabla = new ControlConsumoEntities())
                 {
                     retorno.Consumos = tabla.Consumoes.Where(p => p.IdEquipo == Equipo && p.IdMaterial == Material && p.Lote == Lot && p.NumeroCaja == BoxNumber)
                     .Select(p => new ReportResult.ConsumoDeMateriales
                     {
                         MaterialCode = p.IdMaterial.Trim(),                        
                         IdEquipo = p.IdEquipo.Trim(),
                         BoxNumber = p.Secuencia,
                         Quantity = p.Cantidad,
                         Lot = p.Lote.Trim()
                     }).ToList();                    
                 }
             }
             catch (Exception ex)
             {                
                 string message = "Hubo un error en la consulta sql";
                 if (ex is SqlException)
                 {
                     throw new Exception(message);
                 }
                 else
                 {
                     throw new HttpException(404, "Recurso no encontrado " + ex);
                 }                
                 //return null;
             }
            return retorno;
        }

        public static ReportResult GetPesoResult(String Equipo, String Material, Byte TurnID)
        {
            var retorno = new ReportResult();

            try
            {
                var fecha = DateTime.Now.Date;

                using (var tabla = new SodiQubeDbEntities())
                {
                    retorno.Details = tabla.Details.Where(p => p.TypeID == "P" && p.ProductID == Material)
                    .Select(p => new ReportResult.DetailsResult
                    {
                        ProductID = p.ProductID.Trim(),
                        TypeID = p.TypeID.Trim(),
                        ParametroID = p.ParametroID.Trim(),
                        Value = p.Value,
                        SubTypeID = p.SubTypeID,
                        IsVisible = p.Display
                    }).ToList();

                    retorno.Transactions = tabla.QualityProceseds.Where(p => p.MachineCode == Equipo && p.Product == Material && p.Turno == TurnID && p.Fecha == fecha)
                    .OrderBy(p => p.Fecha2)
                    .ToArray()
                    .Select(p => new ReportResult.TransactionResult
                    {
                        Value = p.Peso.Value,
                        ValueRange = p.RangoPeso.Value,
                        Tick = p.Fecha2.Value.Ticks,
                        Values = tabla.QualityProcesedDetails.Where(d => d.SodimatId == p.SodimatId && d.CycleId == p.CycleId && d.MachineCode == Equipo && d.Fecha == fecha && d.Turno == TurnID).Select(d => d.Peso.Value).ToList()
                    }).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return retorno;
        }

        public static ReportResult GetDiametroResult(String Equipo, String Material, Byte TurnID)
        {
            var retorno = new ReportResult();

            try
            {
                var fecha = DateTime.Now.Date;

                using (var tabla = new SodiQubeDbEntities())
                {
                    retorno.Details = tabla.Details.Where(p => p.TypeID == "D" && p.ProductID == Material)
                    .Select(p => new ReportResult.DetailsResult
                    {
                        ProductID = p.ProductID.Trim(),
                        TypeID = p.TypeID.Trim(),
                        ParametroID = p.ParametroID.Trim(),
                        Value = p.Value,
                        SubTypeID = p.SubTypeID,
                        IsVisible = p.Display
                    }).ToList();

                    retorno.Transactions = tabla.QualityProceseds.Where(p => p.MachineCode == Equipo && p.Product == Material && p.Turno == TurnID && p.Fecha == fecha)
                    .OrderBy(p => p.Fecha2)
                    .ToArray()
                    .Select(p => new ReportResult.TransactionResult
                    {
                        Value = p.Diametro.Value,
                        ValueRange = p.RangoDiametro.Value,
                        Tick = p.Fecha2.Value.Ticks,
                        Values = tabla.QualityProcesedDetails.Where(d => d.SodimatId == p.SodimatId && d.CycleId == p.CycleId && d.MachineCode == Equipo && d.Fecha == fecha && d.Turno == TurnID).Select(d => d.Diametro.Value).ToList()
                    }).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return retorno;
        }

        public static ReportResult GetTiroResult(String Equipo, String Material, Byte TurnID)
        {
            var retorno = new ReportResult();

            try
            {
                var fecha = DateTime.Now.Date;

                using (var tabla = new SodiQubeDbEntities())
                {
                    retorno.Details = tabla.Details.Where(p => p.TypeID == "T" && p.ProductID == Material)
                    .Select(p => new ReportResult.DetailsResult
                    {
                        ProductID = p.ProductID.Trim(),
                        TypeID = p.TypeID.Trim(),
                        ParametroID = p.ParametroID.Trim(),
                        Value = p.Value,
                        SubTypeID = p.SubTypeID,
                        IsVisible = p.Display
                    }).ToList();

                    retorno.Transactions = tabla.QualityProceseds.Where(p => p.MachineCode == Equipo && p.Product == Material && p.Turno == TurnID && p.Fecha == fecha)
                    .OrderBy(p => p.Fecha2)
                    .ToArray()
                    .Select(p => new ReportResult.TransactionResult
                    {
                        Value = p.Tiro.Value,
                        ValueRange = p.RangoTiro.Value,
                        Tick = p.Fecha2.Value.Ticks,
                        Values = tabla.QualityProcesedDetails.Where(d => d.SodimatId == p.SodimatId && d.CycleId == p.CycleId && d.MachineCode == Equipo && d.Fecha == fecha && d.Turno == TurnID).Select(d => d.Tiro.Value).ToList()
                    }).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return retorno;
        }

    }
}
