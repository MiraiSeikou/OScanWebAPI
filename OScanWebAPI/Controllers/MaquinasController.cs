using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OScanWebAPI.Models;

namespace OScanWebAPI.Controllers
{
    public class MaquinasController : ApiController
    {
        private dbHammerspaceEntities db = new dbHammerspaceEntities();

        public MaquinasController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        [Route("api/Maquinas/IdUsuario/{idUsuario}")]
        public IQueryable<Maquina> GetMaquinaByUsuario(int idUsuario)
        {
            return db.Maquina.Where(m => m.IdUsuario == idUsuario);
        }

        // GET: api/Maquinas/5
        [ResponseType(typeof(Maquina))]
        [Route("api/Maquinas/Id/{id}")]
        public IHttpActionResult GetMaquina(int id)
        {
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return NotFound();
            }

            return Ok(maquina);
        }

        // GET: api/Maquinas/5
        [ResponseType(typeof(Maquina))]
        [Route("api/Maquinas/serial/{serial}/{id}")]
        public IHttpActionResult GetMaquinaBySerial(string serial, int id)
        {
            Maquina maquina = db.Maquina.FirstOrDefault(m => m.Serial == serial && m.IdUsuario == id);

            if (maquina == null)
            {
                return Content(HttpStatusCode.NoContent, "");
            }

            return Ok(maquina);
        }

        // PUT: api/Maquinas/5
        [ResponseType(typeof(void))]
		[Route("api/Maquinas/{id}/{maquina}
        public IHttpActionResult PutMaquina(int id, Maquina maquina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != maquina.Id)
            {
                return BadRequest();
            }

			db.Attach(maquina);
            db.Entry(maquina).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
				return Ok;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Maquinas
        [ResponseType(typeof(Maquina))]
        public IHttpActionResult PostMaquina(Maquina maquina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Maquina.Add(maquina);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = maquina.Id }, maquina);
        }

        // DELETE: api/Maquinas/5
        [ResponseType(typeof(Maquina))]
        public IHttpActionResult DeleteMaquina(int id)
        {
            Maquina maquina = db.Maquina.Find(id);
            if (maquina == null)
            {
                return NotFound();
            }

            db.Maquina.Remove(maquina);
            db.SaveChanges();

            return Ok(maquina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaquinaExists(int id)
        {
            return db.Maquina.Count(e => e.Id == id) > 0;
        }
    }
}