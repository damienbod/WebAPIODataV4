using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using WebAppODataV4.Database;

namespace WebAppODataV4
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Address>("Address");
            builder.EntitySet<AddressType>("AddressType");
            builder.EntitySet<BusinessEntity>("BusinessEntity");
            builder.EntitySet<BusinessEntityAddress>("BusinessEntityAddress");
            builder.EntitySet<BusinessEntityContact>("BusinessEntityContact");
            builder.EntitySet<ContactType>("ContactType");
            builder.EntitySet<CountryRegion>("CountryRegion");
            builder.EntitySet<EmailAddress>("EmailAddress");
            builder.EntitySet<Password>("Password");
            builder.EntitySet<Person>("Person");
            builder.EntitySet<PersonPhone>("PersonPhone");
            builder.EntitySet<PhoneNumberType>("PhoneNumberType");
            builder.EntitySet<StateProvince>("StateProvince");

            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
