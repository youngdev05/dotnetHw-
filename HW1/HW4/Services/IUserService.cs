using HW4.Models;

namespace HW4.Services;

public interface IUserService
{
    User? Login(UserLoginDto loginDto);
    User? CreateUser(UserCreateDto userDto);
    User? UpdateUser(int id, UserUpdateDto userDto);
    bool DeleteUser(int id);
    User? GetUserById(int id);
    IEnumerable<User> GetUsers(DateTime? from, DateTime? to);
    IEnumerable<User> GetAllUsers(); 
}