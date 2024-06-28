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
    public class ChangePhoneNumberCommandHandlerTests : TestConfig
    {
        private readonly ICommandHandler<ChangeEmployeePhoneNumberCommand> sut;
        private readonly EmployeeWriteRepository employeeWriteRepository;

        public ChangePhoneNumberCommandHandlerTests()
        {
            sut = Setup<ICommandHandler<ChangeEmployeePhoneNumberCommand>>();
            employeeWriteRepository = Setup<EmployeeWriteRepository>();
        }

        [Fact]
        public async Task Handle_ShouldChangePhoneNumber_WhenValidationPasses()
        {
            var employee = new Employee(
                new EmployeeId(Guid.NewGuid().ToString()),
                new FullName("Hassan", "ahmadi"),
                new NationalCode("1234567890"),
                new PhoneNumber("0913214567"));
            await employeeWriteRepository.Add(employee);
            var command = new ChangeEmployeePhoneNumberCommand
            {
                EmployeeId = employee.Id.Value,
                PhoneNumber = "0987654321"
            };

            await sut.Handle(command);

            var actualResult = await employeeWriteRepository
                .Find(employee.Id);
            actualResult.Should().NotBeNull();
            actualResult!.PhoneNumber.Value.Should().Be(command.PhoneNumber);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
        {
            var command = new ChangeEmployeePhoneNumberCommand
            {
                EmployeeId = Guid.NewGuid().ToString(),
                PhoneNumber = "0987654321"
            };

            var actualResult = () => sut.Handle(command);

            await actualResult.Should().ThrowExactlyAsync<DomainException>();
        }
    }
}
