using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.ApiRest.Controllers
{
    public class FacturaSBOController : ApiController
    {
        //// GET: api/FacturaSBO
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //public IHttpActionResult Get()
        //{
        //    Clases.Documento documento = new Clases.Documento();
        //    documento.DocEntry = 123;
        //    documento.DocNum = 222;
        //    Clases.Detalle[] detalle = {
        //        new Clases.Detalle() { DocEntry = 123, LineNum = 0, ItemCode = "A" },
        //        new Clases.Detalle() { DocEntry = 123, LineNum = 1, ItemCode = "B" }
        //    };
        //    documento.Detalle = detalle;
        //    return Ok<Clases.Documento>(documento);
        //}

        //// GET: api/FacturaSBO/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public IHttpActionResult Get(int? id)
        {
            //Clases.Documento documento = new Clases.Documento();
            //documento.DocEntry = id;
            //documento.DocNum = id * 2;
            //return Ok<Clases.Documento>(documento);
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.ObtenerFactura(id.ToString());
            return Ok<Clases.Respuesta>(result);
        }

        //// POST: api/FacturaSBO
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/FacturaSBO/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/FacturaSBO/5
        //public void Delete(int id)
        //{
        //}
    }
}
