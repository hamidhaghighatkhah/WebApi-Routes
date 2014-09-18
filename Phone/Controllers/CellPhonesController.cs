using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI;
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
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }
            if (HttpContext.Current.Request.Files.Count > 0 &&
                HttpContext.Current.Request.Files[0].ContentLength > 0)
            {
                HttpPostedFile fileData = HttpContext.Current.Request.Files[0];

                string extention = Path.GetExtension(fileData.FileName);

                if (string.Equals(".xls", extention) || string.Equals(".xlsx", extention))
                {
                    string temp_path = HttpContext.Current.Server.MapPath(
                        string.Format("~/Images/{0}{1}", Guid.NewGuid(), extention));

                    fileData.SaveAs(temp_path);
                    cellPhone.ImageUrl = temp_path;
                }
            }
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

        // POST api/Users/Logos
        //لوگوی کاربر
        [Route("Logos")]
        [ResponseType(typeof(IEnumerable<Pair>))]
        public async Task<bool> PostLogosAsync()
        {
            if (Request.Content.IsMimeMultipartContent("form-data"))
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~/uploads/UserLogos");

                var streamProvider = new MultipartFormDataStreamProvider(uploadPath);

                await Request.Content.ReadAsMultipartAsync(streamProvider);

                List<Pair> messages = new List<Pair>();
                foreach (var file in streamProvider.FileData)
                {
                    FileInfo fi = new FileInfo(file.LocalFileName);
                    messages.Add(new Pair(fi.Name, file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty)));
                }

                //if (true/*_biz.AddUserLogo(messages)*/)
                //{
                //    //return new OperationResult
                //    //{
                //    //    type = OperationResultType.Success,
                //    //    value = messages
                //    //};
                //}
                //return new OperationResult
                //{
                //    type = OperationResultType.NotAcceptable,
                //    value = null
                //};
            }
            //return new OperationResult
            //{
            //    type = OperationResultType.Unknown,
            //    value = null
            //};
            return true;
        }


    }
}