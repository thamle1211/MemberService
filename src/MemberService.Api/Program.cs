using System.Text;
using FluentValidation;
using MediatR;
using MemberService.Api.Middleware;
using MemberService.Application.Common.Behaviors;
using MemberService.Application.Common.Interfaces;
using MemberService.Application.Members.Commands.CreateMember;
using MemberService.Infrastructure;
using MemberService.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------
// 1. Add Controllers
// ------------------------------
builder.Services.AddControllers();

// ------------------------------
// 2. Application Layer
// ------------------------------
builder.Services.AddMediatR(typeof(CreateMemberCommand).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CreateMemberCommand).Assembly);

// Add MediatR pipeline behavior (Validation)
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// ------------------------------
// 3. Infrastructure Layer
// ------------------------------
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection"));

// ------------------------------
// 4. Custom Services
// ------------------------------
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IOperatorService, OperatorService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// ------------------------------
// 5. Authentication & Authorization
// ------------------------------
var jwtSection = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSection["Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// ------------------------------
// 6. Build App
// ------------------------------
var app = builder.Build();

// ------------------------------
// 7. Middleware Pipeline
// ------------------------------
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ------------------------------
// 8. Run App
// ------------------------------
await app.RunAsync();
