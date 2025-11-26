
namespace MemberService.Application.Common.Interfaces;

public interface IExternalAgeService
{
    Task<int?> GetPredictedAgeAsync(string name, CancellationToken ct);
}
