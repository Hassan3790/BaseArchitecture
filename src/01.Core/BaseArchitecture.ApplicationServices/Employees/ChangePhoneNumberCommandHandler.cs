using BaseArchitecture.ApplicationServices.Employees.Commands;
using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ValueObjects;
using Framework.Domain;
using Framework.Domain.Exceptions;

namespace BaseArchitecture.ApplicationServices.Employees
{
    public class ChangePhoneNumberCommandHandler(
        EmployeeWriteRepository employeeWriteRepository,
        UnitOfWork unitOfWork) : ICommandHandler<ChangeEmployeePhoneNumberCommand>
    {
        public async Task Handle(ChangeEmployeePhoneNumberCommand command)
        {
            var employee = await employeeWriteRepository
                .Find(new EmployeeId(command.EmployeeId));

            PreventChangePhoneNumberWhenEmployeeNotFound(employee);

            employee!
                .ChangePhoneNumber(new PhoneNumber(command.PhoneNumber));

            await unitOfWork.Complete();
        }

        private static void PreventChangePhoneNumberWhenEmployeeNotFound(Employee? employee)
        {
            if (employee is null)
            {
                throw new DomainException("employee not found");
            }
        }
    }
}
