using MediatR;
using MemberService.Application.Common.Interfaces.Persistence;

namespace MemberService.Application.Members.Queries.SearchMembers;

public class SearchMembersQueryHandler 
    : IRequestHandler<SearchMembersQuery, SearchMembersResult>
{
    private readonly IMemberRepository _repo;

    public SearchMembersQueryHandler(IMemberRepository repo)
    {
        _repo = repo;
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

        return new SearchMembersResult
        {
            Items = items,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
        };
    }
}
