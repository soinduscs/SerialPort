using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.ApiRest.Controllers
{
    public class AlmacenesSBOController : ApiController
    {
        public IHttpActionResult Get()
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.ObtenerAlmacenes();
            return Ok<List<Clases.Maestro>>(result);
        }

        public IHttpActionResult Get(string Codigo)
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.ObtenerAlmacen(Codigo.ToString());
            return Ok<Clases.Maestro>(result);
        }
    }
}
