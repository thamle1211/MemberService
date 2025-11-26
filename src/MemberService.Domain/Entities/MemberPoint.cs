namespace MemberService.Domain.Entities;

public class MemberPoint
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public int Balance { get; set; }
    public List<PointTransaction> Transactions { get; set; } = new();
}

public class PointTransaction
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public int Amount { get; set; }
    public string Type { get; set; } // "ADD", "REDEEM"
    public DateTime CreatedAt { get; set; }
}