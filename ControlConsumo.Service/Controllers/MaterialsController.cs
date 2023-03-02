using ControlConsumo.Service.Model;
using ControlConsumo.Service.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ControlConsumo.Service.Controllers
{
    public class MaterialsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetMaterialConsumido(String Equipo, String Material, String Lot, Int64 BoxNumber)
        {
            var retorno = ReportModel.GetMaterialResult(Equipo, Material, Lot, BoxNumber);
            return this.Request.CreateResponse(HttpStatusCode.OK, retorno);
        }
    }
}
