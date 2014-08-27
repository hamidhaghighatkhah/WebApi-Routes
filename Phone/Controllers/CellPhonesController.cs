using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Phone.Models;

namespace Phone.Controllers
{
    [RoutePrefix("api/CellPhones")]
    public class CellPhonesController : ApiController
    {
        private AppContext db = new AppContext();

        // GET: api/CellPhones
        public IQueryable<CellPhone> GetCellPhones()
        {
            return db.CellPhones;
        }

        // GET: api/CellPhones/5
        [Route("{id:int:min(1)}/GetCellPhonesById")]
        [ResponseType(typeof(CellPhone))]
        public async Task<IHttpActionResult> GetCellPhoneById(int id)
        {
            CellPhone cellPhone = await db.CellPhones.FindAsync(id);
            if (cellPhone == null)
            {
                return NotFound();
            }

            return Ok(cellPhone);
        }

        
        // GET: api/CellPhones/HTC
        /// <summary>
        /// This action can't be reached because the first part of Routes is the same => get  GetCellPhonesById like GetCellPhonesByName
        /// you can't access above action too because of same reason
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("{id:int:min(1)}/GetCellPhonesByName")]
        [ResponseType(typeof(CellPhone))]
        public async Task<IHttpActionResult> GetCellPhoneByName(string name)
        {
            CellPhone cellPhone = await db.CellPhones.FindAsync(name);
            if (cellPhone == null)
            {
                return NotFound();
            }

            return Ok(cellPhone);
        }

        // PUT: api/CellPhones/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCellPhone(int id, CellPhone cellPhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cellPhone.Id)
            {
                return BadRequest();
            }

            db.Entry(cellPhone).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CellPhoneExists(id))
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

        // POST: api/CellPhones
        [ResponseType(typeof(CellPhone))]
        public async Task<IHttpActionResult> PostCellPhone(CellPhone cellPhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CellPhones.Add(cellPhone);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cellPhone.Id }, cellPhone);
        }

        // DELETE: api/CellPhones/5
        [ResponseType(typeof(CellPhone))]
        public async Task<IHttpActionResult> DeleteCellPhone(int id)
        {
            CellPhone cellPhone = await db.CellPhones.FindAsync(id);
            if (cellPhone == null)
            {
                return NotFound();
            }

            db.CellPhones.Remove(cellPhone);
            await db.SaveChangesAsync();

            return Ok(cellPhone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CellPhoneExists(int id)
        {
            return db.CellPhones.Any(e => e.Id == id);
        }
    }
}