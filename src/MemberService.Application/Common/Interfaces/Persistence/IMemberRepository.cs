using MemberService.Application.Members.Model;
using MemberService.Domain.Entities;

namespace MemberService.Application.Common.Interfaces.Persistence;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id);
    Task<List<MemberDto>> GetAllAsync();
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
    Task DeleteAsync(Member member);
    Task<(List<MemberDto> Items, int TotalCount)> SearchAsync(
        string? keyword,
        string sortBy,
        string sortOrder,
        int page,
        int pageSize);
    Task<MemberDto?> GetByEmailAsync(string email);

}