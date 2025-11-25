using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MemberService.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MemberService.Infrastructure.Services;

public class OperatorService : IOperatorService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OperatorService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserName =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.UniqueName);

}