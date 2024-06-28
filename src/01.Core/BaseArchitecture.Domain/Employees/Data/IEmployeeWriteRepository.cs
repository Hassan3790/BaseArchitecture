using BaseArchitecture.Domain.Employees.ValueObjects;
using Framework.Domain;

namespace BaseArchitecture.Domain.Employees.Data;

public interface IEmployeeWriteRepository : WriteRepository
{
    Task Add(Employee employee);
    Task Update(Employee employee);
    Task<Employee?> Find(EmployeeId employeeId);
    Task<bool> IsExistNationalCode(NationalCode nationalCode);
}