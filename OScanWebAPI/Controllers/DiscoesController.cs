﻿using System;
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
    public class DiscoesController : ApiController
    {
        private dbHammerspaceEntities db = new dbHammerspaceEntities();

        public DiscoesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

             // GET: api/Discoes
        public IQueryable<Disco> GetDisco()
        {
            return db.Disco;
        }

        // GET: api/Discoes/idMaquina
        [Route("api/Discoes/IdMaquina/{idMaquina}")]
        public IQueryable<Disco> GetAllDiscoes(int idMaquina)
        {
            return db.Disco.Where(d => d.IdMaquina.Equals(idMaquina));
        }

        [Route("api/Disco/IdMaquina/{idMaquina}")]
        public IHttpActionResult GetDiscoes(int idMaquina)
        {
            return Ok(db.Disco.Last(d => d.IdMaquina.Equals(idMaquina)));
        }

        // GET: api/Discoes/5
        //[ResponseType(typeof(Disco))]
        //public IHttpActionResult GetDisco(int id)
        //{
        //    Disco disco = db.Disco.Find(id);
        //    if (disco == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(disco);
        //}

        // PUT: api/Discoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDisco(int id, Disco disco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disco.Id)
            {
                return BadRequest();
            }

            db.Entry(disco).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscoExists(id))
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

        // POST: api/Discoes
        [ResponseType(typeof(Disco))]
        public IHttpActionResult PostDisco(Disco disco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Disco.Add(disco);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = disco.Id }, disco);
        }

        // DELETE: api/Discoes/5
        [ResponseType(typeof(Disco))]
        public IHttpActionResult DeleteDisco(int id)
        {
            Disco disco = db.Disco.Find(id);
            if (disco == null)
            {
                return NotFound();
            }

            db.Disco.Remove(disco);
            db.SaveChanges();

            return Ok(disco);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscoExists(int id)
        {
            return db.Disco.Count(e => e.Id == id) > 0;
        }
    }
}