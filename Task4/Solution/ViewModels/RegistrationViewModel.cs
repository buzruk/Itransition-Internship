namespace Task4.ViewModels;

public class RegistrationViewModel
{
  [Required]
  [Display(Name = "Name")]
  [StringLength(50)]
  public string Name { get; set; } = string.Empty;

  [Required]
  [DataType(DataType.EmailAddress)]
  [Display(Name = "Email")]
  public string Email { get; set; } = string.Empty;

  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "Password")]
  [Compare("ConfirmPassword")]
  public string Password { get; set; } = string.Empty;

  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "Confirm Password")]
  public string ConfirmPassword { get; set; } = string.Empty;
}
