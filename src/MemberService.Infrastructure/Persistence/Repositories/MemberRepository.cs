
using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Application.Members.Model;
using MemberService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemberService.Infrastructure.Persistence.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _db;

    public MemberRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Member?> GetByIdAsync(Guid id)
    {
        return await _db.Members.FindAsync(id);   
    }

    public async Task<List<MemberDto>> GetAllAsync()
        => await _db.Members.Select(m => new MemberDto
        {
            Id = m.Id,
            FirstName = m.FirstName,
            LastName = m.LastName,
            Email = m.Email,
            BirthDate = m.BirthDate
        })
        .ToListAsync();

    public async Task AddAsync(Member member)
    {
        _db.Members.Add(member);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Member member)
    {
        _db.Members.Update(member);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Member member)
    {
        _db.Members.Remove(member);
        await _db.SaveChangesAsync();
    }

    public async Task<(List<MemberDto> Items, int TotalCount)> SearchAsync(
        string? keyword,
        string sortBy,
        string sortOrder,
        int page,
        int pageSize)
    {
        var query = _db.Members.AsQueryable();

        // Filter
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x => 
                x.FirstName.Contains(keyword) ||
                x.Email.Contains(keyword) ||
                x.LastName.Contains(keyword));
        }

        // Sorting
        query = (sortBy.ToLower(), sortOrder.ToLower()) switch
        {
            ("name", "asc")  => query.OrderBy(x => x.FirstName),
            ("name", "desc") => query.OrderByDescending(x => x.FirstName),

            ("email", "asc")  => query.OrderBy(x => x.Email),
            ("email", "desc") => query.OrderByDescending(x => x.Email),

            ("createdat", "asc")  => query.OrderBy(x => x.LastName),
            _                     => query.OrderByDescending(x => x.LastName)
        };

        // Total Count before paging
        var total = await query.CountAsync();

        // Paging
        var items = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(m => new MemberDto
        {
            Id = m.Id,
            FirstName = m.FirstName,
            LastName = m.LastName,
            Email = m.Email,
            BirthDate = m.BirthDate
        })
        .ToListAsync();

        return (items, total);
    }

    public async Task<MemberDto?> GetByEmailAsync(string email)
    {
        return await _db.Members
        .Where(x => x.Email == email)
        .Select(m => new MemberDto
        {
            Id = m.Id,
            FirstName = m.FirstName,
            LastName = m.LastName,
            Email = m.Email,
            BirthDate = m.BirthDate
        })
        .FirstOrDefaultAsync();
    }
}

