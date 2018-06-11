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
    public class FileStoresController : ApiController
    {
        private dbHammerspaceEntities db = new dbHammerspaceEntities();

        public FileStoresController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Discoes/idMaquina
        [Route("api/Discoes/IdMaquina/{idMaquina}")]
        public IQueryable<FileStore> GetAllDiscoes(int idMaquina)
        {
            return db.FileStore.Where(d => d.IdMaquina == idMaquina);
        }

        [Route("api/Discoes/Id/{idMaquina}")]
        public IHttpActionResult GetDiscoes(int idMaquina)
        {
            return Ok(db.FileStore.OrderByDescending(d => d.Momentum).First(d => d.IdMaquina == idMaquina));
        }

        // PUT: api/FileStores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFileStore(int id, FileStore fileStore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fileStore.Id)
            {
                return BadRequest();
            }

            db.Entry(fileStore).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileStoreExists(id))
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

        // POST: api/FileStores
        [ResponseType(typeof(FileStore))]
        public IHttpActionResult PostFileStore(FileStore fileStore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FileStore.Add(fileStore);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fileStore.Id }, fileStore);
        }

        // DELETE: api/FileStores/5
        [ResponseType(typeof(FileStore))]
        public IHttpActionResult DeleteFileStore(int id)
        {
            FileStore fileStore = db.FileStore.Find(id);
            if (fileStore == null)
            {
                return NotFound();
            }

            db.FileStore.Remove(fileStore);
            db.SaveChanges();

            return Ok(fileStore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FileStoreExists(int id)
        {
            return db.FileStore.Count(e => e.Id == id) > 0;
        }
    }
}