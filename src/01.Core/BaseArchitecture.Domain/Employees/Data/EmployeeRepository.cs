using BaseArchitecture.Domain.Employees.ValueObjects;
using Framework.Domain;

namespace BaseArchitecture.Domain.Employees.Data;

public interface EmployeeRepository : Repository
{
    Task Add(Employee employee);
    Task<bool> IsExistNationalCode(NationalCode nationalCode);
}