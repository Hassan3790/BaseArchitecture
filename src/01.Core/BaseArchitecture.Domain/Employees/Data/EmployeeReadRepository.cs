using BaseArchitecture.Domain.Employees.ViewModels;
using Framework.Domain;

namespace BaseArchitecture.Domain.Employees.Data
{
    public interface EmployeeReadRepository : ReadRepository
    {
        Task<IEnumerable<GetAllEmployeeViewModel>> GetAllEmployees();
    }
}
