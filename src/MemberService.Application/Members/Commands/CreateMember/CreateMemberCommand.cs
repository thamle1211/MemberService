using MediatR;

namespace MemberService.Application.Members.Commands.CreateMember;
public class CreateMemberCommand : IRequest<Guid>
{
    public CreateMemberCommand(string firstName, string lastName, string email, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
    }

    public string FirstName { get; set;}
    public string LastName { get; set;}
    public string Email { get; set;}
    public DateTime BirthDate { get; set; }
}
