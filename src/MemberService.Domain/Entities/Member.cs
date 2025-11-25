namespace MemberService.Domain.Entities;

public class Member : AuditableEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }

    public Member() {}
    public Member(string firstName, string lastName, string email, DateTime birthDate, string operatorId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
        CreatedBy = operatorId;
        CreatedAt = DateTime.UtcNow;
    }
    public void Update(string firstName, string lastName, string email, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
    }
}