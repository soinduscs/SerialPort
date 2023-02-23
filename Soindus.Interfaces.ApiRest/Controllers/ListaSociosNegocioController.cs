using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.ApiRest.Controllers
{
    public class ListaSociosNegocioController : ApiController
    {
        // GET: api/ListaSociosNegocio
        public IHttpActionResult Get()
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.ObtenerListaSociosNegocio("");
            return Ok(result);
        }

        // GET: api/ListaSociosNegocio/S
        public IHttpActionResult Get(string ClienteAduana)
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.ObtenerListaSociosNegocio(ClienteAduana);
            return Ok(result);
        }
    }
}
