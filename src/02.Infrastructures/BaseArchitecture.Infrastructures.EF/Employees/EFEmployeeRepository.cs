using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BaseArchitecture.Infrastructures.EF.Employees;

public class EFEmployeeRepository(EFDataContext context) : EmployeeRepository
{
    public async Task Add(Employee employee)
    {
        context.Add(employee);
        await context.SaveChangesAsync();
    }

    public async Task<bool> IsExistNationalCode(NationalCode nationalCode)
    {
        return await context
            .Set<Employee>()
            .AnyAsync(q => q.NationalCode == nationalCode);
        
    }
}