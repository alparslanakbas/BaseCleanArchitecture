using BaseCleanArchitecture.Application.Employees;
using CD.Results;
using MediatR;
using System.Threading;

namespace BaseCleanArchitecture.API.Modules.Employees
{
    public static class EmployeeModule
    {
        public static void RegisterEmployeeRoutes(this IEndpointRouteBuilder endpoints)
        {
            RouteGroupBuilder group = endpoints.MapGroup("/employees").WithTags("Employees");

            group.MapPost(string.Empty,
                async (ISender sender, EmployeeCreateCommand request, CancellationToken cancellationToken) =>
                {
                    var response = await sender.Send(request, cancellationToken);
                    return Results.Json(response, statusCode: (int)response.StatusCode);
                })
                .Produces<Result<string>>();
                

        }
    }
}
