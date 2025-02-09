using BaseCleanArchitecture.Domain.Abstractions;
using BaseCleanArchitecture.Domain.Employees;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCleanArchitecture.Application.Employees
{
    public sealed record EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

    public sealed class EmployeeGetAllQueryResponse : BaseEntityDto
    {
        private static readonly Random _random = new Random();

        public EmployeeGetAllQueryResponse()
        {
            LastName = GenerateDefaultLastName();
        }

        public string FirstName { get; set; } = "Guest";
        public string LastName { get; set; }
        public string FullName => string.Join(" ", FirstName, LastName);
        public DateOnly BirthOfDate { get; set; }
        public decimal Salary { get; set; }
        public string IdentityNumber { get; set; } = default!;

        private string GenerateDefaultLastName()
        {
            return $"{FirstName}{_random.Next(100000, 999999)}";
        }
    }

    internal sealed class EmployeeGetAllQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
    {
        public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
        {
            var response = employeeRepository.GetAll()
                .Select( s => new EmployeeGetAllQueryResponse
                {
                    Id = s.Id,
                    IdentityNumber = s.PersonelInformation.IdentityNumber,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    BirthOfDate = s.BirthOfDate,
                    Salary = s.Salary,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate,
                    IsDelete = s.IsDelete,
                    DeleteAt = s.DeleteAt
                })
                .AsQueryable();

            return Task.FromResult(response);

        }
    }
}
