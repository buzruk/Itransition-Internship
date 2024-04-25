namespace Task4.Models;

public class User
{
  public int Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string Email { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;

  public DateTime LastLoginDate { get; set; }

  public DateTime CreatedDate { get; set; } = DateTime.Now;

  public Status UserStatus { get; set; }

}
