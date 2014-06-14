using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using WebAppODataV4.Database;

namespace WebAppODataV4.Controllers
{
    [ODataRoutePrefix("ContactType")]
    public class ContactTypeController : ODataController
    {
        readonly DomainModel _db = new DomainModel();

        [ODataRoute()]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult Get()
        {
            return Ok(_db.ContactType.AsQueryable());
        }

        [ODataRoute()]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.ContactType.Find(key));
        }

        [ODataRoute()]
        [HttpPost]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult Post(ContactType contactType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.ContactType.AddOrUpdate(contactType);
            _db.SaveChanges();
            return Created(contactType);
        }

        [ODataRoute("({key})")]
        [HttpPut]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult Put([FromODataUri] int key, ContactType contactType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != contactType.ContactTypeID)
            {
                return BadRequest();
            }

            _db.ContactType.AddOrUpdate(contactType);
            _db.SaveChanges();
            // Updated is not supported yet, you should implement the correct response in a real app
            return Ok(contactType);
        }

        [ODataRoute()]
        [HttpDelete]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var entityInDb = _db.ContactType.SingleOrDefault(t => t.ContactTypeID == key);
            _db.ContactType.Remove(entityInDb);
            _db.SaveChanges();

            // No Content not supported yet
            return Ok("Deleted");
        }

        [ODataRoute()]
        [HttpPatch]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ContactType> delta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactType = _db.ContactType.Single(t => t.ContactTypeID == key);
            delta.Patch(contactType);
            _db.SaveChanges();
            // Updated is not supported yet, you should implement the correct response in a real app
            return Ok(contactType);
        }

        /// <summary>
        /// This is a Odata Action for complex data changes...
        /// </summary>
        [HttpPost]
        [ODataRoute("Default.ChangePersonStatus")]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult ChangePersonStatus(ODataActionParameters parameters)
        {
            if (ModelState.IsValid)
            {
                var level = parameters["Level"];
                // SAVE THIS TO THE DATABASE OR WHATEVER....
            }

            return Ok(true);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}

