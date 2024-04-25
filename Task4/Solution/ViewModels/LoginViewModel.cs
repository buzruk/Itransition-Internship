namespace Task4.ViewModels;

public class LoginViewModel
{
  [Required]
  [DataType(DataType.EmailAddress)]
  [Display(Name = "Email")]
  public string Email { get; set; } = string.Empty;

  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "Password")]
  public string Password { get; set; } = string.Empty;
}
