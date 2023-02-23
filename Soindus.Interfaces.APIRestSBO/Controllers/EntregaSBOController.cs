using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Soindus.Interfaces.APIRestSBO.Controllers
{
    public class EntregaSBOController : ApiController
    {
        //// GET: api/EntregaSBO
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public IHttpActionResult Get()
        {
            Clases.Documento documento = new Clases.Documento();
            documento.DocEntry = 123;
            documento.DocNum = 222;
            Clases.Detalle[] detalle = { 
                new Clases.Detalle() { DocEntry = 123, LineNum = 0, ItemCode = "A" }, 
                new Clases.Detalle() { DocEntry = 123, LineNum = 1, ItemCode = "B" } 
            };
            documento.Detalle = detalle;
            return Ok<Clases.Documento>(documento);
        }

        // GET: api/EntregaSBO/5
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
            var result = integra.ObtenerEntrega(id.ToString());
            return Ok<Clases.Documento>(result);
        }

        public class MyJson
        {
            public string Base64 { get; set; }
        }

        //// POST: api/EntregaSBO
        //public HttpResponseMessage Post(MyJson value)
        //{
        //    MyJson x = value;
        //    //return Ok<MyJson>(x);
        //    return Request.CreateResponse<MyJson>(HttpStatusCode.OK, x);
        //}
        // POST: api/EntregaSBO
        public IHttpActionResult Post([FromBody] Clases.Documento value)
        {
            //Clases.Documento x = value;
            //return Ok<Clases.Documento>(x);
            SBO.IntegracionSBO integra = new SBO.IntegracionSBO();
            var result = integra.IntegrarEntrega(value);
            return Ok<Clases.Respuesta>(result);
        }

        // PUT: api/EntregaSBO/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EntregaSBO/5
        public void Delete(int id)
        {
        }
    }
}
