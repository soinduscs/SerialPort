using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.ApiRest.Controllers
{
    public class ListaFacturasController : ApiController
    {
        // GET: api/ListaFacturas/CR76174723
        public IHttpActionResult Get(string CardCode)
        {
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.ObtenerListaFacturas(CardCode.ToString());
            return Ok(result);
        }
    }
}
