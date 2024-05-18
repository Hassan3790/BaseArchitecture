using Microsoft.AspNetCore.Mvc;

namespace _04.EndPoints.API.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    [HttpPost]
    public IActionResult Add()
    {
        return Ok();
    }
}