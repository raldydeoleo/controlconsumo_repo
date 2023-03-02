using ControlConsumo.Service.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlConsumo.Service.Model
{
    public class DetailsModel
    {
        public static IEnumerable<ReportResult.DetailsResult> GetDetails(String TypeID, String ProductID)
        {
            try 
            {
                using (var tabla = new SodiQubeDBEntities())
                {
                    //tabla.SP_PESO("M01");

                    return tabla.Details.Where(p => p.TypeID == TypeID && p.ProductID == ProductID).Select(p => new ReportResult.DetailsResult
                    {
                        ProductID = p.ProductID,
                        TypeID = p.TypeID,
                        ParametroID = p.ParametroID,
                        Value = p.Value

                    }).ToList();
                }
            }catch(Exception ex)
            {
                return null;
            }
        }
        public static IEnumerable<ReportResult.TransactionResult> GetTransactions()
        {

            try 
            {
                using (var tabla = new SodiQubeDBEntities())
                {
                    var result = tabla.SP_PESO("M01");
                    return tabla.QualityStats.Where(p => p.StatId == 0).Select(u => new ReportResult.TransactionResult
                    {
                        Weight =(float) u.Weight 

                    }).ToList(); 
                }

            }catch(Exception ex)
            {
                return null;        
            }            
        }

    }
}
