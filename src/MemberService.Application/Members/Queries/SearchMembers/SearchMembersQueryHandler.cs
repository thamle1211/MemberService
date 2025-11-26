using MediatR;
using MemberService.Application.Common.Interfaces;
using MemberService.Application.Common.Interfaces.Persistence;

namespace MemberService.Application.Members.Queries.SearchMembers;

public class SearchMembersQueryHandler 
    : IRequestHandler<SearchMembersQuery, SearchMembersResult>
{
    private readonly IMemberRepository _repo;
    private readonly IExternalAgeService _ageService;


    public SearchMembersQueryHandler(IMemberRepository repo,
                                    IExternalAgeService ageService)
    {
        _repo = repo;
        _ageService = ageService;
    }

    public async Task<SearchMembersResult> Handle(SearchMembersQuery request, CancellationToken ct)
    {
        var (items, totalCount) = await _repo.SearchAsync(
            request.Keyword,
            request.SortBy,
            request.SortOrder,
            request.Page,
            request.PageSize
        );

        foreach (var item in items)
        {
            var age = await _ageService.GetPredictedAgeAsync(item.FirstName, ct);

            if (age != null)
            {
                item.FirstName = $"{item.FirstName} (Predicted age: {age})";
            }
        }


        return new SearchMembersResult
        {
            Items = items,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
        };
    }
}
