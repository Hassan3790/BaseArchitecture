using BaseArchitecture.ApplicationServices.Employees;
using BaseArchitecture.ApplicationServices.Employees.Commands;
using Microsoft.AspNetCore.Mvc;

namespace _04.EndPoints.API.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    [HttpPost]
    public async Task Register(
        [FromServices] RegisterEmployeeHandler handler,
        [FromBody] RegisterEmployeeCommand command)
    {
        await handler.Handle(command);
    }
}