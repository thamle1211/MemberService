namespace MemberService.Application.Members.Common;

public class MemberDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime BirthDate { get; set; }
}