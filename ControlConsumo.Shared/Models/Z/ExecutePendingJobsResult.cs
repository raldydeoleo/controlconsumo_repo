using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    public class ExecutePendingJobsResult
    {
        public Int32 ZCount { get; set; }
        public Int32 TraysProductsCount { get; set; }
        public Int32 ProductsRoutesCount { get; set; }
        public Int32 SyncroCount { get; set; }
        public Int32 MaterialsCount { get; set; }
        public Int32 UnitsCount { get; set; }
        public Int32 RelaseCount { get; set; }
        public Int32 RelasePositionCount { get; set; }
        public Int32 ConfigCount { get; set; }
        public Int32 ConsumptionsCount { get; set; }
        public Int32 ElaboratesCount { get; set; }
        public Int32 ConfigMaterialsCount { get; set; }
        public Int32 UsersCount { get; set; }
        public Boolean HasWorked { get { return ConsumptionsCount + ElaboratesCount + ConfigCount + TraysProductsCount + ProductsRoutesCount + SyncroCount + MaterialsCount + UnitsCount + RelaseCount + RelasePositionCount + ZCount + ConfigMaterialsCount + UsersCount > 0; } }
    }
}
