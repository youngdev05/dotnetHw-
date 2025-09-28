using HW1.Models;

namespace HW1.Services;

public class UserStatistics
{
    private readonly IEnumerable<User> _users;

    public UserStatistics(IEnumerable<User> users)
    {
        _users = users;
    }

    // 1️⃣ Минимальная дата регистрации
    public DateTime GetMinRegistrationDate()
    {
        return _users.Min(u => u.CreatedDate);
    }

    // 2️⃣ Максимальная дата регистрации
    public DateTime GetMaxRegistrationDate()
    {
        return _users.Max(u => u.CreatedDate);
    }

    // 3️⃣ Отсортированный список пользователей
    public IEnumerable<User> GetSortedUsers()
    {
        return _users.OrderBy(u => u.Username);
    }

    // 4️⃣ Фильтрация по полу
    public IEnumerable<User> GetUsersByGender(string gender)
    {
        return _users
            .Where(u => u.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase));
    }

    // 5️⃣ Общее число пользователей
    public int GetUserCount()
    {
        return _users.Count();
    }

    // ⭐ 6️⃣ Пагинация — вывод нужного диапазона (например, 30–40)
    public IEnumerable<User> GetPagedUsers(int start, int count)
    {
        return _users
            .OrderBy(u => u.Id)
            .Skip(start)
            .Take(count);
    }
}
