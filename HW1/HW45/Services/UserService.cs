using HW4.Data;
using HW4.Models;
using Microsoft.EntityFrameworkCore;

namespace HW4.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User? Login(UserLoginDto loginDto)
    {
        return _context.Users
            .FirstOrDefault(u => 
                u.Username == loginDto.Username && 
                u.Password == loginDto.Password);
    }

    public User? CreateUser(UserCreateDto userDto)
    {
        if (_context.Users.Any(u => 
                u.Username.Equals(userDto.Username, StringComparison.OrdinalIgnoreCase)))
        {
            return null;
        }

        var user = new User
        {
            Username = userDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Gender = "Unknown",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User? UpdateUser(int id, UserUpdateDto userDto)
    {
        var user = _context.Users.Find(id);
        if (user == null) return null;

        user.Username = userDto.Username;
        user.Password = userDto.Password;
        user.UpdatedDate = DateTime.UtcNow;

        _context.SaveChanges();
        return user;
    }

    public bool DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }

    public User? GetUserById(int id)
    {
        return _context.Users.Find(id);
    }

    public IEnumerable<User> GetUsers(DateTime? from, DateTime? to)
    {
        var query = _context.Users.AsQueryable();

        if (from.HasValue)
            query = query.Where(u => u.CreatedDate >= from.Value);

        if (to.HasValue)
            query = query.Where(u => u.CreatedDate <= to.Value);

        return query.ToList();
    }

    public IEnumerable<User> GetAllUsers() => _context.Users.ToList();
}