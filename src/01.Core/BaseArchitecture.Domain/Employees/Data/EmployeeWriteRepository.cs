using BaseArchitecture.Domain.Employees.ValueObjects;
using Framework.Domain;

namespace BaseArchitecture.Domain.Employees.Data;

public interface EmployeeWriteRepository : WriteRepository
{
    Task Add(Employee employee);
    Task<Employee?> Find(EmployeeId employeeId);
    Task<bool> ExistNationalCode(NationalCode nationalCode);
}