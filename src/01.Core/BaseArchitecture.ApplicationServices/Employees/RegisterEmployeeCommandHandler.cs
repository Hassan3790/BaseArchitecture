using BaseArchitecture.ApplicationServices.Employees.Commands;
using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ValueObjects;
using Framework.Domain;
using Framework.Domain.Exceptions;

namespace BaseArchitecture.ApplicationServices.Employees;

public class RegisterEmployeeCommandHandler(
    EmployeeWriteRepository employeeWriteRepository,
    UnitOfWork unitOfWork)
    : ICommandHandler<RegisterEmployeeCommand>
{
    public async Task Handle(RegisterEmployeeCommand command)
    {
        await PreventAddWhenNationalCodeIsDuplicate(command);

        var employee = new Employee(
            new EmployeeId(Guid.NewGuid().ToString()),
            new FullName(command.FirstName, command.LastName),
            new NationalCode(command.NationalCode),
            new PhoneNumber(command.PhoneNumber));

        await employeeWriteRepository.Add(employee);
        await unitOfWork.Complete();
    }

    private async Task PreventAddWhenNationalCodeIsDuplicate(
        RegisterEmployeeCommand command)
    {
        var isExist = await employeeWriteRepository
            .ExistNationalCode(new NationalCode(command.NationalCode));
        if (isExist)
        {
            throw new DomainException("duplicate national code");
        }
    }
}