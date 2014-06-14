using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using WebAppODataV4.Database;

namespace WebAppODataV4.Controllers
{
    [ODataRoutePrefix("Person")]
    public class PersonController : ODataController
    {
        readonly DomainModel _db = new DomainModel();

        [ODataRoute]
        [EnableQuery(PageSize = 20, AllowedQueryOptions= AllowedQueryOptions.All  )]
        public IHttpActionResult Get()
        {  
            return Ok(_db.Person.AsQueryable());
        }

        [ODataRoute("({key})")]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.Person.Find(key));
        }

        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        [ODataRoute("Default.MyFirstFunction")]
        [HttpGet]
        public IHttpActionResult MyFirstFunction()
        {
            return Ok(_db.Person.Where(t => t.FirstName.StartsWith("K")));
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
