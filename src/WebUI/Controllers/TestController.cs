using Microsoft.AspNetCore.Mvc;
using ProFilePOC2.Application.Common.Security;

namespace WebUI.Controllers;

public class TestController : ApiControllerBase
{
    // [Authorize(Roles = )]
    [HttpGet]
    public IActionResult Test()
    {
        var result = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(result);
    }
}