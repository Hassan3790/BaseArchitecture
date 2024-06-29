using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BaseArchitecture.Persistence.EF.Employees
{
    public class EfEmployeeReadRepository(ApplicationDbContext context) : EmployeeReadRepository
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
