using Microsoft.AspNetCore.Mvc;

namespace MemberService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwt;

    public AuthController(IJwtService jwt)
    {
        _jwt = jwt;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        //mock, need to go to db to check
        if (request.Username != "admin" || request.Password != "123456")
            return Unauthorized("Invalid credentials");

        var token = _jwt.GenerateToken(
            userId: "1",
            userName: request.Username,
            role: "Operator"
        );

        return Ok(new { token });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}