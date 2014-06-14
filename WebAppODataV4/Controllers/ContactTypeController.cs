using System.Linq;
using System.Web.Http;
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

