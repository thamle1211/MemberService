using FluentValidation.TestHelper;
using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Application.Members.Commands.CreateMember;
using MemberService.Application.Members.Model;
using MemberService.Domain.Entities;
using Moq;

namespace MemberService.Tests.Validators;

public class CreateMemberCommandValidatorTests
{
    private readonly CreateMemberCommandValidator _validator;
    private readonly Mock<IMemberRepository> _repoMock;

    public CreateMemberCommandValidatorTests()
    {
        _repoMock = new Mock<IMemberRepository>();
        _validator = new CreateMemberCommandValidator(_repoMock.Object);
    }

    private static CreateMemberCommand ValidCommand =>
        new CreateMemberCommand(
            "John",
            "Doe",
            "john.doe@example.com",
            new DateTime(1995, 1, 1)
        );

     [Fact]
    public async Task Should_Pass_When_Command_Is_Valid()
    {
        _repoMock
            .Setup(r => r.GetByEmailAsync("john.doe@example.com"))
            .ReturnsAsync((MemberDto)null);

        var result = await _validator.TestValidateAsync(ValidCommand);

        result.ShouldNotHaveAnyValidationErrors();
    }
    [Fact]
    public async Task Should_Have_Error_When_FirstName_Is_Empty()
    {
        var cmd = new CreateMemberCommand("", "Doe", "john@test.com", ValidCommand.BirthDate);

        var result = await _validator.TestValidateAsync(cmd);

        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }
    [Fact]
    public async Task Should_Have_Error_When_LastName_Is_Empty()
    {
        var cmd = new CreateMemberCommand("John", "", "john@test.com", ValidCommand.BirthDate);

        var result = await _validator.TestValidateAsync(cmd);

        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }
    [Fact]
    public async Task Should_Have_Error_When_Email_Is_Invalid()
    {
        var cmd = new CreateMemberCommand("John", "Doe", "invalid-email", ValidCommand.BirthDate);

        var result = await _validator.TestValidateAsync(cmd);

        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public async Task Should_Have_Error_When_Email_Already_Exists()
    {
        _repoMock
            .Setup(r => r.GetByEmailAsync("john.doe@example.com"))
            .ReturnsAsync(new MemberDto { Email = "john.doe@example.com" });

        var result = await _validator.TestValidateAsync(ValidCommand);

        result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("Email must be unique");
    }

    [Fact]
    public async Task Should_Have_Error_When_BirthDate_In_Future()
    {
        var cmd = new CreateMemberCommand("John", "Doe", "john@test.com",
            DateTime.Today.AddDays(1));

        var result = await _validator.TestValidateAsync(cmd);

        result.ShouldHaveValidationErrorFor(x => x.BirthDate)
                .WithErrorMessage("BirthDate must be in the past");
    }

    [Fact]
    public async Task Should_Have_Error_When_Member_Is_Under_18()
    {
        var under18 = DateTime.Today.AddYears(-17);

        var cmd = new CreateMemberCommand("John", "Doe", "john@test.com", under18);

        var result = await _validator.TestValidateAsync(cmd);

        result.ShouldHaveValidationErrorFor(x => x.BirthDate)
                .WithErrorMessage("Member must be at least 18 years old");
    }
}