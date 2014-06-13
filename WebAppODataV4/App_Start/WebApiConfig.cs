using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using Microsoft.Data.Edm;
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

            config.MapODataServiceRoute("odata", "odata", model: GetModel());         
        }

        public static Microsoft.OData.Edm.IEdmModel GetModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

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

            EntitySetConfiguration<Person> persons = builder.EntitySet<Person>("Person");
            
            FunctionConfiguration myFirstFunction = persons.EntityType.Collection.Function("MyFirstFunction");
            myFirstFunction.ReturnsCollectionFromEntitySet<Person>("Person");
    
            ActionConfiguration action = builder.EntityType<PersonPhone>().Action("ChangePersonStatus");
            action.Parameter<string>("Level");
            action.Returns<bool>();
           
            return builder.GetEdmModel();
        }
    }
}
