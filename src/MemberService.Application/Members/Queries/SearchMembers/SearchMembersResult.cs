using MemberService.Application.Members.Common;

namespace MemberService.Application.Members.Queries.SearchMembers;

public class SearchMembersResult
{
    public List<MemberDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
