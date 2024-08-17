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
    public class RegisterEmployeeCommandHandlerTests : TestConfig
    {
        private readonly ICommandHandler<RegisterEmployeeCommand, string> sut;
        private readonly EmployeeWriteRepository employeeWriteRepository;
        private readonly EmployeeReadRepository employeeReadRepository;
        private readonly UnitOfWork unitOfWork;

        public RegisterEmployeeCommandHandlerTests()
        {
            sut = Setup<ICommandHandler<RegisterEmployeeCommand, string>>();
            employeeWriteRepository = Setup<EmployeeWriteRepository>();
            employeeReadRepository = Setup<EmployeeReadRepository>();
            unitOfWork = Setup<UnitOfWork>();
        }

        [Fact]
        public async Task
            Handle_ShouldAddEmployee_WhenNationalCodeIsNotDuplicate()
        {
            var command = new RegisterEmployeeCommand(
                "hassan",
                "ahmadi",
                "1234567890",
                "09123456789");

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
        public async Task
            Handle_ShouldThrowException_WhenNationalCodeIsDuplicate()
        {
            var employee = new Employee(
                new EmployeeId(Guid.NewGuid().ToString()),
                new FullName("hassan", "ahmadi"),
                new NationalCode("1234567890"),
                new PhoneNumber("09123456789"));
            await employeeWriteRepository.Add(employee);
            await unitOfWork.Complete();
            var command = new RegisterEmployeeCommand(
                "hassan",
                "ahmadi",
                "1234567890", 
                "09123456789");

            var actualResult = await sut.Handle(command);

            actualResult.IsError.Should().BeTrue();
            actualResult.Errors.Should().HaveCount(1);
            actualResult.Errors.First().Type.Should().Be(ErrorType.Conflict);
        }
    }
}