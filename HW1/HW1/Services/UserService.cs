using HW1.Models;

namespace HW1.Services;

public class UserService : IUserService
{
    private static readonly List<User> Users = new()
    {
        new User { Id = 1, Username = "user", Password = "user", CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow }
    };

    private static int _nextId = 2;

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
}
