using MediatR;

namespace MemberService.Application.Members.Queries.SearchMembers;

public class SearchMembersQuery : IRequest<SearchMembersResult>
{
    public string? Keyword { get; set; }      // FirstName | LastName | Email

    // Paging
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    
    // Sorting
    public string SortBy { get; set; } = "FirstName";
    public string SortOrder { get; set; } = "desc";
}
