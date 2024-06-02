using BaseArchitecture.Domain.Employees.Events;
using Framework.Domain.Events;

namespace BaseArchitecture.ApplicationServices.Employees
{
    internal class EmployeeCreatedEventHandler : IHandleMessage<EmployeeCreated>
    {
        public Task Handle(EmployeeCreated message)
        {
            Console.WriteLine($"employee added {message.EmployeeId.Value} {message.FullName.FirstName}");
            return Task.CompletedTask;
        }
    }
}
