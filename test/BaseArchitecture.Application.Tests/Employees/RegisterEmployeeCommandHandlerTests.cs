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
    public class RegisterEmployeeCommandHandlerTests : TestConfig
    {
        private readonly ICommandHandler<RegisterEmployeeCommand> sut;
        private readonly EmployeeWriteRepository employeeWriteRepository;
        private readonly EmployeeReadRepository employeeReadRepository;

        public RegisterEmployeeCommandHandlerTests()
        {
            sut = Setup<ICommandHandler<RegisterEmployeeCommand>>();
            employeeWriteRepository = Setup<EmployeeWriteRepository>();
            employeeReadRepository = Setup<EmployeeReadRepository>();
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

            var result = await employeeReadRepository
                .GetAllEmployees();
            var actualResult = result.FirstOrDefault();
            actualResult.Should().NotBeNull();
            actualResult!.FirstName.Should().Be(command.FirstName);
            actualResult.LastName.Should().Be(command.LastName);
            actualResult.NationalCode.Should().Be(command.NationalCode);
            actualResult.PhoneNumber.Should().Be(command.PhoneNumber);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenNationalCodeIsDuplicate()
        {
            var employee = new Employee(
                new EmployeeId(Guid.NewGuid().ToString()),
                new FullName("hassan", "ahmadi"),
                new NationalCode("1234567890"),
                new PhoneNumber("09123456789"));
            await employeeWriteRepository.Add(employee);
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
