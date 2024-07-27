using BaseArchitecture.ApplicationServices.Employees.Commands;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ViewModels;
using ErrorOr;
using Framework.Domain;
using Microsoft.AspNetCore.Mvc;

namespace _04.EndPoints.API.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    [HttpPost]
    public async Task<ErrorOr<string>> Register(
        [FromServices]
        ICommandHandler<RegisterEmployeeCommand, string> handler,
        [FromBody] RegisterEmployeeCommand command)
    {
        return await handler.Handle(command);
    }

    [HttpPatch("phone-number")]
    public async Task<ErrorOr<string>> ChangePhoneNumber(
        [FromServices]
        ICommandHandler<ChangeEmployeePhoneNumberCommand, string>
            handler,
        ChangeEmployeePhoneNumberCommand command)
    {
        return await handler.Handle(command);
    }

    [HttpGet]
    public async Task<IEnumerable<GetAllEmployeeViewModel>> GetAll(
        [FromServices] EmployeeReadRepository employeeReadRepository)
    {
        return await employeeReadRepository.GetAllEmployees();
    }
}