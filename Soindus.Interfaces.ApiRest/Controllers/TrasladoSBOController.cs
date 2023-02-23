using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.ApiRest.Controllers
{
    public class TrasladoSBOController : ApiController
    {
        // GET: api/TrasladoSBO/5
        public IHttpActionResult Get(int? id)
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.ObtenerTraslado(id.ToString());
            return Ok<Clases.Respuesta>(result);
        }

        // POST: api/TrasladoSBO
        public IHttpActionResult Post([FromBody] Clases.Traslado value)
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.IntegrarTraslado(value);
            return Ok<Clases.Respuesta>(result);
        }
    }
}
