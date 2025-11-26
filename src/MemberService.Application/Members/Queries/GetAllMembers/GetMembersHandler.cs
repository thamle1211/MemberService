using MediatR;
using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Application.Members.Model;

namespace MemberService.Application.Members.Queries.GetAllMembers;
public class GetMembersHandler : IRequestHandler<GetMembersQuery, List<MemberDto>>
{
    private readonly IMemberRepository _repo;

    public GetMembersHandler(IMemberRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<MemberDto>> Handle(GetMembersQuery request, CancellationToken ct)
        => await _repo.GetAllAsync();
}
