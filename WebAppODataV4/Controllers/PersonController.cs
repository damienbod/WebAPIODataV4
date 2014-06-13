using System.Linq;
using System.Threading.Tasks;
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

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        [ODataRoute("Default.MyFirstFunction")]
        [HttpGet]
        public IHttpActionResult MyFirstFunction()
        {
            return Ok(_db.Person.Where(t => t.FirstName.StartsWith("K")));
        }

        /// <summary>
        /// This is a Odata Action for complex data changes...
        /// </summary>
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        [ODataRoute("ChangePersonStatus")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePersonStatus([FromODataUri] int key, [FromBody]ODataActionParameters parameters)
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
