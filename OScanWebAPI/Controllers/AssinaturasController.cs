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
    public class AssinaturasController : ApiController
    {
        private dbHammerspaceEntities db = new dbHammerspaceEntities();

        public AssinaturasController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Assinaturas
        public IQueryable<Assinatura> GetAssinatura()
        {
            return db.Assinatura;
        }

        // GET: api/Assinaturas/5
        [ResponseType(typeof(Assinatura))]
        public IHttpActionResult GetAssinatura(int id)
        {
            Assinatura assinatura = db.Assinatura.Find(id);
            if (assinatura == null)
            {
                return NotFound();
            }

            return Ok(assinatura);
        }

        // PUT: api/Assinaturas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssinatura(int id, Assinatura assinatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assinatura.Id)
            {
                return BadRequest();
            }

            db.Entry(assinatura).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssinaturaExists(id))
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

        // POST: api/Assinaturas
        [ResponseType(typeof(Assinatura))]
        public IHttpActionResult PostAssinatura(Assinatura assinatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assinatura.Add(assinatura);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assinatura.Id }, assinatura);
        }

        // DELETE: api/Assinaturas/5
        [ResponseType(typeof(Assinatura))]
        public IHttpActionResult DeleteAssinatura(int id)
        {
            Assinatura assinatura = db.Assinatura.Find(id);
            if (assinatura == null)
            {
                return NotFound();
            }

            db.Assinatura.Remove(assinatura);
            db.SaveChanges();

            return Ok(assinatura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssinaturaExists(int id)
        {
            return db.Assinatura.Count(e => e.Id == id) > 0;
        }
    }
}