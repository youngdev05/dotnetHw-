namespace HW4.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty; 
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
