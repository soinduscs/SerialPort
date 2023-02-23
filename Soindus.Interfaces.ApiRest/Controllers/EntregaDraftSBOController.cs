using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.ApiRest.Controllers
{
    public class EntregaDraftSBOController : ApiController
    {
        // GET: api/EntregaDraftSBO/5
        public IHttpActionResult Get(int? id)
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.ObtenerEntregaDraft(id.ToString());
            return Ok<Clases.Respuesta>(result);
        }

        // POST: api/EntregaDraftSBO
        public IHttpActionResult Post([FromBody] Clases.Documento value)
        {
            //Clases.Documento x = value;
            //return Ok<Clases.Documento>(x);
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.IntegrarEntregaDraft(value);
            return Ok<Clases.Respuesta>(result);
        }
    }
}
