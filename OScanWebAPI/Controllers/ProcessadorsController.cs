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
    public class ProcessadorsController : ApiController
    {
        private dbHammerspaceEntities db = new dbHammerspaceEntities();

        public ProcessadorsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Processadors
        public IQueryable<Processador> GetProcessador()
        {
            return db.Processador;
        }

        // GET: api/Processadors/idMaquina
        [Route("api/Processadors/IdMaquina/{idMaquina}")]
        public IHttpActionResult GetProcessador(int idMaquina)
        {
            Processador processador = db.Processador.Last(p => p.IdMaquina.Equals(idMaquina));
            return Ok(processador);
        }

        // PUT: api/Processadors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProcessador(int id, Processador processador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != processador.Id)
            {
                return BadRequest();
            }

            db.Entry(processador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessadorExists(id))
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

        // POST: api/Processadors
        [ResponseType(typeof(Processador))]
        public IHttpActionResult PostProcessador(Processador processador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Processador.Add(processador);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = processador.Id }, processador);
        }

        // DELETE: api/Processadors/5
        [ResponseType(typeof(Processador))]
        public IHttpActionResult DeleteProcessador(int id)
        {
            Processador processador = db.Processador.Find(id);
            if (processador == null)
            {
                return NotFound();
            }

            db.Processador.Remove(processador);
            db.SaveChanges();

            return Ok(processador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProcessadorExists(int id)
        {
            return db.Processador.Count(e => e.Id == id) > 0;
        }
    }
}