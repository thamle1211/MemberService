using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MemberService.Application.Members.Queries.GetAllMembers;
using MemberService.Application.Members.Commands.CreateMember;
using MemberService.Application.Members.Commands.UpdateMember;
using MemberService.Application.Members.Commands.DeleteMember;
using MemberService.Application.Members.Queries.SearchMembers;

namespace MemberService.Api.Controllers;

[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _mediator.Send(new GetMembersQuery()));

    [Authorize(Roles = "Operator")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberCommand cmd)
        => Ok(await _mediator.Send(cmd));

    [Authorize(Roles = "Operator")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateMemberCommand cmd)
    {
        if (id != cmd.Id) return BadRequest();
        return Ok(await _mediator.Send(cmd));
    }

    [Authorize(Roles = "Operator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
        => Ok(await _mediator.Send(new DeleteMemberCommand(id)));

    [Authorize]
    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchMembersQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}