using BaseCleanArchitecture.Domain.Employees;
using BaseCleanArchitecture.Infrastructure.Context;
using CD.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCleanArchitecture.Infrastructure.Repositories
{
    internal sealed class EmployeeRepository : Repository<Employee, ApplicationDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
