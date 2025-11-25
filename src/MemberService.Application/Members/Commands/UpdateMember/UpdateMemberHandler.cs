using MediatR;
using MemberService.Application.Common.Interfaces.Persistence;


namespace MemberService.Application.Members.Commands.UpdateMember;
public class UpdateMemberHandler : IRequestHandler<UpdateMemberCommand, bool>
{
    private readonly IMemberRepository _repo;

    public UpdateMemberHandler(IMemberRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(UpdateMemberCommand request, CancellationToken ct)
    {
        var member = await _repo.GetByIdAsync(request.Id);
        if (member == null) return false;

        member.Update(request.FirstName, request.LastName, request.Email, request.BirthDate);
        await _repo.UpdateAsync(member);
        return true;
    }
}