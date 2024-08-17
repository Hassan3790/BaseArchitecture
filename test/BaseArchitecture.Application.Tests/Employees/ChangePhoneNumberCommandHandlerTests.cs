using BaseArchitecture.ApplicationServices.Employees.Commands;
using BaseArchitecture.Domain.Employees;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ValueObjects;
using BaseArchitecture.TestTools.Configurations;
using ErrorOr;
using FluentAssertions;
using Framework.Domain;
using Framework.Domain.Exceptions;

namespace BaseArchitecture.Application.Tests.Employees
{
    public class ChangePhoneNumberCommandHandlerTests : TestConfig
    {
        private readonly ICommandHandler<ChangeEmployeePhoneNumberCommand, string> sut;
        private readonly EmployeeWriteRepository employeeWriteRepository;
        private readonly UnitOfWork unitOfWork;

        public ChangePhoneNumberCommandHandlerTests()
        {
            sut = Setup<ICommandHandler<ChangeEmployeePhoneNumberCommand, string>>();
            employeeWriteRepository = Setup<EmployeeWriteRepository>();
            unitOfWork = Setup<UnitOfWork>();
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
            await unitOfWork.Complete();
            var command = new ChangeEmployeePhoneNumberCommand(
                employee.Id.Value,
                "0987654321");

            await sut.Handle(command);

            var actualResult = await employeeWriteRepository
                .Find(employee.Id);
            actualResult.Should().NotBeNull();
            actualResult!.PhoneNumber.Value.Should().Be(command.PhoneNumber);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenEmployeeNotFound()
        {
            var command = new ChangeEmployeePhoneNumberCommand(
                Guid.NewGuid().ToString(),
                "0987654321");

            var actualResult = await sut.Handle(command);

            actualResult.IsError.Should().BeTrue();
            actualResult.Errors.Should().HaveCount(1);
            actualResult.Errors.First().Type.Should().Be(ErrorType.NotFound);
        }
    }
}
