using FluentValidation;
using MemberService.Application.Common.Interfaces.Persistence;

namespace MemberService.Application.Members.Commands.UpdateMember;

public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator(IMemberRepository repo)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Member Id is required");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format invalid")
            .MaximumLength(100)
            .MustAsync(async (command, email, ct) =>
            {
                var existing = await repo.GetByEmailAsync(email);
                return existing == null || existing.Id == command.Id;
            }).WithMessage("Email must be unique");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required")
            .Must(BeInThePast).WithMessage("BirthDate must be in the past")
            .Must(BeAdult).WithMessage("Member must be at least 18 years old");
    }

    private static bool BeInThePast(DateTime date) => date < DateTime.Today;

    private static bool BeAdult(DateTime date)
    {
        var age = DateTime.Today.Year - date.Year;
        if (date.Date > DateTime.Today.AddYears(-age)) age--;
        return age >= 18;
    }
}
