using System.Net.Http.Json;
using MemberService.Application.Common.Interfaces;
using MemberService.Application.Members.Model;

namespace MemberService.Infrastructure.Services;

public class ExternalAgeService : IExternalAgeService
{
    private readonly HttpClient _http;

    public ExternalAgeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<int?> GetPredictedAgeAsync(string name, CancellationToken ct)
    {
        var url = $"https://api.agify.io?name={name}";

        var result = await _http.GetFromJsonAsync<ExternalAgeDto>(url, ct);
        return result?.Age;
    }
}