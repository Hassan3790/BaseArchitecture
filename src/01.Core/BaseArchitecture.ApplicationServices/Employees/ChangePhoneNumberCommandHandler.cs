using BaseArchitecture.ApplicationServices.Employees.Commands;
using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ValueObjects;
using ErrorOr;
using Framework.Domain;
using Framework.Domain.Exceptions;

namespace BaseArchitecture.ApplicationServices.Employees
{
    public class ChangePhoneNumberCommandHandler(
        EmployeeWriteRepository employeeWriteRepository,
        UnitOfWork unitOfWork)
        : ICommandHandler<ChangeEmployeePhoneNumberCommand, string>
    {
        public async Task<ErrorOr<string>> Handle(
            ChangeEmployeePhoneNumberCommand command)
        {
            var employee = await employeeWriteRepository
                .Find(new EmployeeId(command.EmployeeId));

            if (employee is null)
                return Error.NotFound("Employee not found");

            employee!
                .ChangePhoneNumber(new PhoneNumber(command.PhoneNumber));

            await unitOfWork.Complete();

            return employee.Id.Value;
        }
    }
}