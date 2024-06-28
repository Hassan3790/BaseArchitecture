using BaseArchitecture.ApplicationServices.Employees.Commands;
using BaseArchitecture.Domain.Employees.Data;
using BaseArchitecture.Domain.Employees.ViewModels;
using Framework.Domain;
using Microsoft.AspNetCore.Mvc;

namespace _04.EndPoints.API.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    [HttpPost]
    public async Task Register(
        [FromServices] ICommandHandler<RegisterEmployeeCommand> handler,
        [FromBody] RegisterEmployeeCommand command)
    {
        await handler.Handle(command);
    }

    [HttpPatch("phone-number")]
    public async Task ChangePhoneNumber(
        [FromServices] ICommandHandler<ChangeEmployeePhoneNumberCommand> handler,
        ChangeEmployeePhoneNumberCommand command)
    {
        await handler.Handle(command);
    }

    [HttpGet]
    public async Task<IEnumerable<GetAllEmployeeViewModel>> GetAll(
        [FromServices] EmployeeReadRepository employeeReadRepository)
    {
        return await employeeReadRepository.GetAllEmployees();
    }
}