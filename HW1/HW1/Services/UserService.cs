using HW1.Models;

namespace HW1.Services;

public class UserService : IUserService
{
    private static readonly List<User> Users = new()
    {
        new User { Id = 1, Username = "user1", Password = "user", Gender = "Male", CreatedDate = new DateTime(2024, 5, 1), UpdatedDate = DateTime.UtcNow },
        new User { Id = 2, Username = "maria", Password = "123", Gender = "Female", CreatedDate = new DateTime(2024, 5, 3), UpdatedDate = DateTime.UtcNow },
        new User { Id = 3, Username = "alex", Password = "123", Gender = "Male", CreatedDate = new DateTime(2024, 6, 10), UpdatedDate = DateTime.UtcNow },
        new User { Id = 4, Username = "olga", Password = "123", Gender = "Female", CreatedDate = new DateTime(2024, 7, 1), UpdatedDate = DateTime.UtcNow },
        new User { Id = 5, Username = "artem", Password = "123", Gender = "Male", CreatedDate = new DateTime(2024, 8, 20), UpdatedDate = DateTime.UtcNow }
    };

    private static int _nextId = 6;

    public User? Login(UserLoginDto loginDto)
    {
        return Users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
    }

    public User? CreateUser(UserCreateDto userDto)
    {
        if (Users.Any(u => u.Username.Equals(userDto.Username, StringComparison.OrdinalIgnoreCase)))
            return null;

        var user = new User
        {
            Id = _nextId++,
            Username = userDto.Username,
            Password = userDto.Password,
            Gender = "Unknown",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        Users.Add(user);
        return user;
    }

    public User? UpdateUser(int id, UserUpdateDto userDto)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return null;

        user.Username = userDto.Username;
        user.Password = userDto.Password;
        user.UpdatedDate = DateTime.UtcNow;

        return user;
    }

    public bool DeleteUser(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return false;

        Users.Remove(user);
        return true;
    }

    public User? GetUserById(int id)
    {
        return Users.FirstOrDefault(u => u.Id == id);
    }

    public IEnumerable<User> GetUsers(DateTime? from, DateTime? to)
    {
        var query = Users.AsEnumerable();

        if (from.HasValue)
            query = query.Where(u => u.CreatedDate >= from.Value);

        if (to.HasValue)
            query = query.Where(u => u.CreatedDate <= to.Value);

        return query;
    }

    public IEnumerable<User> GetAllUsers() => Users;
}
