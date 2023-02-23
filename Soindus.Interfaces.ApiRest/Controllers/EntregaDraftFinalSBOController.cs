using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.ApiRest.Controllers
{
    public class EntregaDraftFinalSBOController : ApiController
    {
        // PUT: api/EntregaDraftFinalSBO/5
        public IHttpActionResult Put(int? id, [FromBody] string value)
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.IntegrarEntregaDraftFinal(id.ToString());
            return Ok<Clases.Respuesta>(result);
        }
    }
}
