using MediatR;
using MemberService.Application.Common.Interfaces.Persistence;

namespace MemberService.Application.Members.Commands.DeleteMember;
public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IMemberRepository _repo;

    public DeleteMemberHandler(IMemberRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken ct)
    {
        var member = await _repo.GetByIdAsync(request.Id);
        if (member == null) return false;

        await _repo.DeleteAsync(member);
        return true;
    }
}
