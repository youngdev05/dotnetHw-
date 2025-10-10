using HW4.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW4.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto loginDto)
    {
        if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            return BadRequest("Логин и пароль обязательны");

        var user = _userService.Login(loginDto);
        if (user == null)
            return Unauthorized("Неверный логин или пароль");

        return Ok(new { Message = "Авторизация успешна", UserId = user.Id });
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreateDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Password))
            return BadRequest("Имя пользователя и пароль обязательны");

        var createdUser = _userService.CreateUser(userDto);
        if (createdUser == null)
            return Conflict("Пользователь с таким логином уже существует");

        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserUpdateDto userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Username) || string.IsNullOrWhiteSpace(userDto.Password))
            return BadRequest("Имя пользователя и пароль обязательны");

        var updatedUser = _userService.UpdateUser(id, userDto);
        if (updatedUser == null)
            return NotFound("Пользователь не найден");

        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var success = _userService.DeleteUser(id);
        if (!success)
            return NotFound("Пользователь не найден");

        return NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetUsers([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var users = _userService.GetUsers(from, to);
        return Ok(users);
    }

    [HttpGet("stats")]
    public IActionResult GetUserStatistics([FromQuery] string? gender, [FromQuery] int? skip, [FromQuery] int? take)
    {
        var allUsersQuery = _userService.GetAllUsers().AsQueryable();
        var stats = new UserStatistics(allUsersQuery);

        var result = new
        {
            MinDate = stats.GetMinRegistrationDate(),
            MaxDate = stats.GetMaxRegistrationDate(),
            TotalUsers = stats.GetUserCount(),
            Sorted = stats.GetSortedUsers(),
            FilteredByGender = gender != null ? stats.GetUsersByGender(gender) : null,
            Paged = (skip.HasValue && take.HasValue) 
                ? stats.GetPagedUsers(skip.Value, take.Value) 
                : null
        };

        return Ok(result);
    }
}
