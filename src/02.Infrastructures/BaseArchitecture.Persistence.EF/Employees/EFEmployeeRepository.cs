using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ValueObjects;
using Framework.Domain.Entities;
using Framework.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace BaseArchitecture.Persistence.EF.Employees;

public class EFEmployeeRepository(
    EFDataContext context,
    IMessageDispatcher messageDispatcher) : EmployeeRepository
{
    public async Task Add(Employee employee)
    {
        context.Add(employee);
        await context.SaveChangesAsync();
    }

    public async Task Update(Employee employee)
    {
        await context.SaveChangesAsync();
    }

    public async Task<Employee?> Find(EmployeeId employeeId)
    {
        return await context
            .Set<Employee>()
            .SingleOrDefaultAsync(e => e.Id == employeeId);
    }

    public async Task<bool> IsExistNationalCode(NationalCode nationalCode)
    {
        return await context
            .Set<Employee>()
            .AnyAsync(q => q.NationalCode == nationalCode);
    }

    public void RaiseEvent(AggregateRoot entity)
    {
        if (context.Database.CurrentTransaction is null)
        {
            messageDispatcher.Publish(entity.Events);
        }
    }
}