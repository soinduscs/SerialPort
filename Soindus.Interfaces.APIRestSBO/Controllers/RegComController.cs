using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Soindus.Interfaces.Modelos.SOcomData;

namespace Soindus.Interfaces.APIRestSBO.Controllers
{
    public class RegComController : ApiController
    {
        Modelo_SOcomData db = new Modelo_SOcomData();

        // GET: api/RegCom
        public IHttpActionResult Get()
        {
            var result = db.RegCom.Where(q => q.Com_ID != "");
            return Ok<IEnumerable<RegCom>>(result.AsQueryable());
        }

        // GET: api/RegCom/5
        public IHttpActionResult Get(string id)
        {
            var result = db.RegCom.Where(q => q.Com_ID == id);
            if (result.Count() == 0)
            {
                //return StatusCode(HttpStatusCode.NotFound);
                return Ok(new Clases.Message() { Mensaje = "No encontrado" });
            }
            return Ok<RegCom>(result.Single());
        }

        // POST: api/RegCom
        public IHttpActionResult Post([FromBody] RegCom regCom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = db.RegCom.Find(regCom.Com_ID, regCom.User_ID);
            if (entity == null)
            {
                regCom.LastUpdate = regCom.LastUpdate != null ? regCom.LastUpdate : DateTime.Now;
                db.RegCom.Add(regCom);
            }
            else
            {
                entity.User_ID = regCom.User_ID;
                regCom.LastUpdate = regCom.LastUpdate != null ? regCom.LastUpdate : DateTime.Now;
                entity.LastUpdate = regCom.LastUpdate;
                entity.StrValue = regCom.StrValue;
            }
            db.SaveChanges();
            return Ok<RegCom>(regCom);
        }

        // PUT: api/RegCom/5
        public IHttpActionResult Put(string id, [FromBody]string value)
        {
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: api/RegCom/5
        public IHttpActionResult Delete(int id)
        {
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
