using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.ApiRest.Controllers
{
    public class LCSocioNegocioController : ApiController
    {
        public IHttpActionResult Get([FromUri] Clases.LCParams queryData)
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            string CardCode = queryData.CardCode.ToString();
            double MontoTrx = queryData.MontoTrx;
            var result = integra.ObtenerLCSocioNegocio(CardCode, MontoTrx);
            return Ok(result);
        }
    }
}
