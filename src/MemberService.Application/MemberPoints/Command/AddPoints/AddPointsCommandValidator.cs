using FluentValidation;

namespace MemberService.Application.MemberPoints.Commands.AddPoints;

public class AddPointsCommandValidator 
    : AbstractValidator<AddPointsCommand>
{
    public AddPointsCommandValidator()
    {
        RuleFor(x => x.MemberId).NotEmpty();
        RuleFor(x => x.Points).GreaterThan(0);
    }
}
