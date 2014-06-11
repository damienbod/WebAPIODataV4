using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using WebAppODataV4.Database;

namespace WebAppODataV4.Controllers
{
    public class AddressTypeController : ODataController
    {
        readonly DomainModel _db = new DomainModel();

        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get()
        {
            return Ok(_db.AddressType.AsQueryable());
        }

        public IHttpActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.AddressType.SingleOrDefault(t => t.AddressTypeID == key));
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}

