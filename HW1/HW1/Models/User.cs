using HW1.Models;

namespace HW1.Services;

public interface IUserService
{
    User? Login(UserLoginDto loginDto);
    User? CreateUser(UserCreateDto userDto);
    User? UpdateUser(int id, UserUpdateDto userDto);
    bool DeleteUser(int id);
    User? GetUserById(int id);
    IEnumerable<User> GetUsers(DateTime? from, DateTime? to);
}
