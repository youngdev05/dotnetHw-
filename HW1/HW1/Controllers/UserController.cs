using HW1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW1.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private static List<User> Users = new List<User>
    {
        new User { Id = 1, Username = "user", Password = "user" }
    };

    private static int _nextId = 2;

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto loginDto)
    {
        var user = Users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
        if (user == null)
            return Unauthorized("Неверный логин или пароль");

        return Ok(new { Message = "Авторизация успешна", UserId = user.Id });
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreateDto userDto)
    {
        var user = new User
        {
            Id = _nextId++,
            Username = userDto.Username,
            Password = userDto.Password
        };

        Users.Add(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserUpdateDto userDto)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound("Пользователь не найден");

        user.Username = userDto.Username;
        user.Password = userDto.Password;

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound("Пользователь не найден");

        Users.Remove(user);
        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }
}