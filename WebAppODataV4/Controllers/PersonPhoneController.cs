using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using WebAppODataV4.Database;

namespace WebAppODataV4.Controllers
{
    public class PersonPhoneController : ODataController
    {
        readonly DomainModel _db = new DomainModel();
        private static readonly ODataValidationSettings _validationSettings = new ODataValidationSettings();

        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get()
        {
            return Ok(_db.PersonPhone.AsQueryable());
        }

        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.PersonPhone.Find(key));
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
