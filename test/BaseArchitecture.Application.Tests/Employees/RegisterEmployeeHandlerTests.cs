using BaseArchitecture.ApplicationServices.Employees.Commands;
using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ValueObjects;
using BaseArchitecture.TestTools.Configurations;
using FluentAssertions;
using Framework.Domain;
using Framework.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BaseArchitecture.Application.Tests.Employees
{
    public class RegisterEmployeeHandlerTests : TestConfig
    {
        private readonly ICommandHandler<RegisterEmployeeCommand> sut;
        private readonly EmployeeRepository employeeRepository;

        public RegisterEmployeeHandlerTests()
        {
            sut = Setup<ICommandHandler<RegisterEmployeeCommand>>();
            employeeRepository = Setup<EmployeeRepository>();
        }

        [Fact]
        public async Task Handle_ShouldAddEmployee_WhenNationalCodeIsNotDuplicate()
        {
            var command = new RegisterEmployeeCommand
            {
                FirstName = "hassan",
                LastName = "ahmadi",
                NationalCode = "1234567890",
                PhoneNumber = "09123456789"
            };

            await sut.Handle(command);

            var actualResult = await readDataContext
                .Set<Employee>()
                .SingleOrDefaultAsync();
            actualResult.Should().NotBeNull();
            actualResult!.FullName.FirstName.Should().Be(command.FirstName);
            actualResult.FullName.LastName.Should().Be(command.LastName);
            actualResult.NationalCode.Value.Should().Be(command.NationalCode);
            actualResult.PhoneNumber.Value.Should().Be(command.PhoneNumber);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenNationalCodeIsDuplicate()
        {
            var employee = new Employee(
                new EmployeeId(Guid.NewGuid().ToString()),
                new FullName("hassan", "ahmadi"),
                new NationalCode("1234567890"),
                new PhoneNumber("09123456789"));
            await employeeRepository.Add(employee);
            var command = new RegisterEmployeeCommand
            {
                FirstName = "hassan",
                LastName = "ahmadi",
                NationalCode = "1234567890",
                PhoneNumber = "09123456789"
            };

            var actualResult = () => sut.Handle(command);

            await actualResult.Should().ThrowExactlyAsync<DomainException>();
        }
    }
}
