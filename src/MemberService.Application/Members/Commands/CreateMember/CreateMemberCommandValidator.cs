using FluentValidation;
using MemberService.Application.Common.Interfaces.Persistence;

namespace MemberService.Application.Members.Commands.CreateMember;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    private readonly IMemberRepository _repo;

    public CreateMemberCommandValidator(IMemberRepository repo)
    {
        _repo = repo;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .MaximumLength(100).WithMessage("FirstName cannot exceed 100 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .MaximumLength(100).WithMessage("LastName cannot exceed 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid")
            .MaximumLength(100)
            .MustAsync(UniqueEmail).WithMessage("Email must be unique");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required")
            .Must(BeInThePast).WithMessage("BirthDate must be in the past")
            .Must(BeAdult).WithMessage("Member must be at least 18 years old");

    }

    private static bool BeInThePast(DateTime date)
    {
        return date < DateTime.Today;
    }

    // Check age >= 18
    private static bool BeAdult(DateTime date)
    {
        var age = DateTime.Today.Year - date.Year;
        if (date.Date > DateTime.Today.AddYears(-age)) age--;
        return age >= 18;
    }

    // Custom rule: check email unique
    private async Task<bool> UniqueEmail(string email, CancellationToken ct)
    {
        var existing = await _repo.GetByEmailAsync(email);
        return existing == null;
    }
}