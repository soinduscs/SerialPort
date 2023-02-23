using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.ApiRest.Controllers
{
    public class EntregaDraftStatusSBOController : ApiController
    {
        // GET: api/EntregaDraftStatusSBO/5
        public IHttpActionResult Get(int? id)
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.ObtenerEntregaDraftStatus(id.ToString());
            return Ok(result);
        }
    }
}
