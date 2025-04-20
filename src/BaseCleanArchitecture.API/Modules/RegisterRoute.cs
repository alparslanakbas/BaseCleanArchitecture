using BaseCleanArchitecture.API.Modules.Employees;

namespace BaseCleanArchitecture.API.Modules
{
    public static class RegisterRoute
    {
        public static void RegisterRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.RegisterEmployeeRoutes();
        }
    }
}
