using Bogus;
using HW4.Data;
using HW4.Models;

namespace HW4.Services;

public class TestDataService
{
    private readonly AppDbContext _context;

    public TestDataService(AppDbContext context)
    {
        _context = context;
    }

    public void SeedTestUsers(int count = 10)
    {
        if (_context.Users.Any()) return; // Не добавлять, если уже есть данные

        var genders = new[] { "Male", "Female", "Other" };
        var testUsers = new Faker<User>()
            .RuleFor(u => u.Id, f => 0) // EF Core сам назначит ID
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => BCrypt.Net.BCrypt.HashPassword(f.Internet.Password(8, false)))
            .RuleFor(u => u.Gender, f => f.PickRandom(genders))
            .RuleFor(u => u.CreatedDate, f => f.Date.Past(2))
            .RuleFor(u => u.UpdatedDate, f => f.Date.Recent(30))
            .Generate(count);

        _context.Users.AddRange(testUsers);
        _context.SaveChanges();
    }
}