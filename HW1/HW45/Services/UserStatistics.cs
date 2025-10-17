using HW4.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HW4.Services;

public class UserStatistics
{
    private readonly IQueryable<User> _users;

    public UserStatistics(IQueryable<User> users)
    {
        _users = users;
    }

    public DateTime GetMinRegistrationDate()
    {
        return _users.Min(u => u.CreatedDate);
    }

    public DateTime GetMaxRegistrationDate()
    {
        return _users.Max(u => u.CreatedDate);
    }

    public IEnumerable<User> GetSortedUsers()
    {
        return _users.OrderBy(u => u.Username).ToList();
    }

    public IEnumerable<User> GetUsersByGender(string gender)
    {
        return _users
            .Where(u => EF.Functions.Like(u.Gender, gender))
            .ToList();
    }

    public int GetUserCount()
    {
        return _users.Count();
    }
    
    public IEnumerable<User> GetPagedUsers(int start, int count)
    {
        return _users
            .OrderBy(u => u.Id)
            .Skip(start)
            .Take(count)
            .ToList();
    }
}