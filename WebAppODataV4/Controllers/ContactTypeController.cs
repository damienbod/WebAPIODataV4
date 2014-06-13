using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using WebAppODataV4.Database;

namespace WebAppODataV4.Controllers
{
    public class ContactTypeController : ODataController
    {
        readonly DomainModel _db = new DomainModel();

        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.Count)]
        public IHttpActionResult Get()
        {
            return Ok(_db.ContactType.AsQueryable());
        }

        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.Count)]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.ContactType.Find(key));
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}

