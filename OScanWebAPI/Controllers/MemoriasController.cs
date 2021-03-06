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
    public class MemoriasController : ApiController
    {
        private dbHammerspaceEntities db = new dbHammerspaceEntities();

        public MemoriasController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Memorias/{idMaquina}
        [Route("api/Memorias/IdMaquina/{idMaquina}")]
        public IQueryable<Memoria> GetAlltMemorias(int idMaquina)
        {
            return db.Memoria.Where(m => m.IdMaquina == idMaquina);
        }

        [Route("api/Memorias/Id/{idMaquina}")]
        public IHttpActionResult GetLastMemorias(int idMaquina)
        {
            return Ok(db.Memoria.OrderByDescending(d => d.Momentum).First(d => d.IdMaquina == idMaquina));
        }

        // PUT: api/Memorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMemoria(int id, Memoria memoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != memoria.Id)
            {
                return BadRequest();
            }

            db.Entry(memoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemoriaExists(id))
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

        // POST: api/Memorias
        [ResponseType(typeof(Memoria))]
        public IHttpActionResult PostMemoria(Memoria memoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Memoria.Add(memoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = memoria.Id }, memoria);
        }

        // DELETE: api/Memorias/5
        [ResponseType(typeof(Memoria))]
        public IHttpActionResult DeleteMemoria(int id)
        {
            Memoria memoria = db.Memoria.Find(id);
            if (memoria == null)
            {
                return NotFound();
            }

            db.Memoria.Remove(memoria);
            db.SaveChanges();

            return Ok(memoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MemoriaExists(int id)
        {
            return db.Memoria.Count(e => e.Id == id) > 0;
        }
    }
}