using BaseCleanArchitecture.Application.Employees;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace BaseCleanArchitecture.API.Controllers
{
    [Route("odata")]
    [ApiController]
    [EnableQuery]
    public class AppODataController(ISender sender) : ODataController
    {

        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new();
            builder.EnableLowerCamelCase();
            builder.EntitySet<EmployeeGetAllQueryResponse>("employees");
            return builder.GetEdmModel();
        }

        [HttpGet("employees")]
        public async Task<IQueryable> GetAllEmployees(CancellationToken cancellationToken)
        {
            var response = await sender.Send(new EmployeeGetAllQuery(), cancellationToken);
            return response;
        }
    }
}
