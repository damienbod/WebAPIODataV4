using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.OData;
using WebAppODataV4.Database;

namespace WebAppODataV4.Controllers
{
    public class PersonController : ODataController
    {
        readonly DomainModel _db = new DomainModel();

        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get()
        {  
            return Ok(_db.Person.AsQueryable());
        }

        public IHttpActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.Person.SingleOrDefault(t => t.BusinessEntityID == key));
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
