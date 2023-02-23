using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using Soindus.Interfaces.Modelos.SOcomData;
using Microsoft.Data.OData;

namespace Soindus.Interfaces.APIRestSBO.Controllers
{
    /*
    Puede que la clase WebApiConfig requiera cambios adicionales para agregar una ruta para este controlador. Combine estas instrucciones en el método Register de la clase WebApiConfig según corresponda. Tenga en cuenta que las direcciones URL de OData distinguen mayúsculas de minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Soindus.Interfaces.Modelos.SOcomData;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<RegCom>("RegComOdata");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RegComOdataController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        Modelo_SOcomData db = new Modelo_SOcomData();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        // GET: odata/RegComOdata
        public IHttpActionResult GetRegComOdata(ODataQueryOptions<RegCom> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
            IEnumerable<RegCom> result = db.RegCom.Where(q => q.Com_ID != "");
            return Ok<IEnumerable<RegCom>>(result.AsQueryable());
            // return Ok<IEnumerable<RegCom>>(tipoCambiarios);
            //return StatusCode(HttpStatusCode.NotImplemented);
        }

        // GET: odata/RegComOdata('Com_ID')
        public IHttpActionResult GetRegCom([FromODataUri] string key, ODataQueryOptions<RegCom> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
            IEnumerable<RegCom> result = db.RegCom.Where(q => q.Com_ID == key);
            if (result.Count() == 0)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            return Ok<RegCom>(result.Single());
            // return Ok<TipoCambiario>(tipoCambiario);
            //return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/RegComOdata
        public IHttpActionResult Post(RegCom regCom)
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
            //return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/RegComOdata(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<RegCom> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(regCom);

            // TODO: Save the patched entity.

            // return Updated(regCom);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/RegComOdata(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<RegCom> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(regCom);

            // TODO: Save the patched entity.

            // return Updated(regCom);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/RegComOdata(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
