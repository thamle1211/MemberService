using FluentValidation;
namespace MemberService.Application.Members.Queries.SearchMembers;

public class SearchMembersQueryValidator : AbstractValidator<SearchMembersQuery>
{
    public SearchMembersQueryValidator()
    {
        RuleFor(x => x.Keyword)
            .MaximumLength(100).WithMessage("Keyword cannot exceed 100 characters");

        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100");

        RuleFor(x => x.SortOrder)
            .Must(x => string.IsNullOrEmpty(x) || x.ToLower() == "asc" || x.ToLower() == "desc")
            .WithMessage("SortOrder must be 'asc' or 'desc'");
    }
}
