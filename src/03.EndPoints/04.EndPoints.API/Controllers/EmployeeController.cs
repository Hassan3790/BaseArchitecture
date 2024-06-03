using BaseArchitecture.ApplicationServices.Employees.Commands;
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
}