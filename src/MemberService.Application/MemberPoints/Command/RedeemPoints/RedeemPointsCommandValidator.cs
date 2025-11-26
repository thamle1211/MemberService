using FluentValidation;

namespace MemberService.Application.MemberPoints.Commands.RedeemPoints;

public class RedeemPointsCommandValidator 
    : AbstractValidator<RedeemPointsCommand>
{
    public RedeemPointsCommandValidator()
    {
        RuleFor(x => x.MemberId).NotEmpty();
        RuleFor(x => x.Points).GreaterThan(0);
    }
}
