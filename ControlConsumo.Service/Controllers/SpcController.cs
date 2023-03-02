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
    public class SpcController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetPeso(String Equipo, String Material, Byte TurnID)
        {
            var retorno = ReportModel.GetPesoResult(Equipo, Material, TurnID);
            return this.Request.CreateResponse(HttpStatusCode.OK, retorno);
        }     

        [HttpGet]
        public HttpResponseMessage GetDiametro(String Equipo, String Material, Byte TurnID)
        {
            var retorno = ReportModel.GetDiametroResult(Equipo, Material, TurnID);
            return this.Request.CreateResponse(HttpStatusCode.OK, retorno);
        }

        [HttpGet]
        public HttpResponseMessage GetTiro(String Equipo, String Material, Byte TurnID)
        {
            var retorno = ReportModel.GetTiroResult(Equipo, Material, TurnID);
            return this.Request.CreateResponse(HttpStatusCode.OK, retorno);
        }        
    }
}
