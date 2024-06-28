using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ViewModels;
using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseArchitecture.Persistence.EF.Employees
{
    public class EfEmployeeReadRepository(EFDataContext context) : EmployeeReadRepository
    {
        public async Task<IEnumerable<GetAllEmployeeViewModel>> GetAllEmployees()
        {
            return await context
                .Set<Employee>()
                .AsNoTracking()
                .Select(e => new GetAllEmployeeViewModel
                {
                    Id = e.Id.Value,
                    FirstName = e.FullName.FirstName,
                    LastName = e.FullName.LastName,
                    NationalCode = e.NationalCode.Value,
                    PhoneNumber = e.PhoneNumber.Value
                }).ToListAsync();
        }
    }
}
