using ControlConsumo.Service.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlConsumo.Service.Model
{
    public class ReportModel
    {
        public static ReportResult GetPesoResult(String Equipo, String Material, Byte TurnID)
        {
            var retorno = new ReportResult();

            try
            {
                var fecha = DateTime.Now.Date;

                using (var tabla = new SodiQubeDBEntities())
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
            catch (Exception ex)
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

                using (var tabla = new SodiQubeDBEntities())
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
            catch (Exception ex)
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

                using (var tabla = new SodiQubeDBEntities())
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
            catch (Exception ex)
            {
                return null;
            }

            return retorno;
        }

    }
}
