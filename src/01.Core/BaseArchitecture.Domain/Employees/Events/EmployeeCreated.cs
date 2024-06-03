using BaseArchitecture.Domain.Employees.ValueObjects;
using Framework.Domain.Events;

namespace BaseArchitecture.Domain.Employees.Events
{
    public class EmployeeCreated : IDomainEvent
    {
        public EmployeeCreated(EmployeeId employeeId, FullName fullName)
        {
            EmployeeId = employeeId;
            FullName = fullName;
        }
        public EmployeeId EmployeeId { get; set; }
        public FullName FullName { get; set; }
    }
}
