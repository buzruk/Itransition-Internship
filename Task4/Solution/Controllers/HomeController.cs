using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Task4.Data;

namespace Task4.Controllers;

public class HomeController : Controller
{
  private readonly AppDbContext _dbContext;

  public HomeController(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  [Authorize]
  public async Task<IActionResult> Index()
  {
    if (await IsUserNotExistAsync())
      return RedirectToAction("Login");
    else if (await IsUserBlockedAsync())
      return RedirectToAction("Blocked");

    return View(await _dbContext.Users.ToListAsync());
  }

  public IActionResult Login()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Login(LoginViewModel loginViewModel)
  {
    if (ModelState.IsValid)
    {
      if (_dbContext.Users.Select(u => u.Email).Any(em => em == loginViewModel.Email))
      {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(u =>
          u.Email == loginViewModel.Email && u.Password == loginViewModel.Password);

        if (user is not null)
        {
          List<Claim> claims = [new Claim(ClaimTypes.Name, user.Name)];
          ClaimsIdentity claimsIdentity = new(claims, "Cookies");
          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
          user.LastLoginDate = DateTime.Now;
          await _dbContext.SaveChangesAsync();
          return RedirectToAction("Index");
        }
      }

      return RedirectToAction("Index");
    }
    //return View();
    return View(loginViewModel);
  }

  public IActionResult Register()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Register(RegistrationViewModel registrationViewModel)
  {
    if (ModelState.IsValid)
    {
      if (string.IsNullOrEmpty(registrationViewModel.Email) || string.IsNullOrEmpty(registrationViewModel.Password)
    || _dbContext.Users.Select(u => u.Email).Contains(registrationViewModel.Email) || _dbContext.Users.Select(u => u.Name).Contains(registrationViewModel.Name))
      {
        ViewBag.Error = "Register error";
        return View();
      }
      User user = new()
      {
        Name = registrationViewModel.Name,
        Email = registrationViewModel.Email,
        Password = registrationViewModel.Password,
        UserStatus = Status.Active
      };
      _dbContext.Users.Add(user);
      await _dbContext.SaveChangesAsync();
      return RedirectToAction("Login");
    }
    //return View();
    return View(registrationViewModel);
  }

  public IActionResult Blocked() => View();

  public IActionResult UserNotExist() => View();

  [Authorize]
  public async Task<IActionResult> Block(int[] userId)
  {
    if (await IsUserNotExistAsync())
      return RedirectToAction("Login");
    else if (await IsUserBlockedAsync())
      return RedirectToAction("Blocked");

    foreach (int id in userId)
    {
      if (_dbContext.Users.Select(u => u.Id).Contains(id))
      {
        User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        user.UserStatus = Status.Blocked;
        await _dbContext.SaveChangesAsync();
      }
    }

    return RedirectToAction("Index");
  }

  [Authorize]
  public async Task<IActionResult> Unblock(int[] userId)
  {
    if (await IsUserNotExistAsync())
      return RedirectToAction("Login");
    else if (await IsUserBlockedAsync())
      return RedirectToAction("Blocked");

    foreach (int id in userId)
    {
      if (_dbContext.Users.Select(u => u.Id).Contains(id))
      {
        User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        user.UserStatus = Status.Active;
        await _dbContext.SaveChangesAsync();
      }
    }

    return RedirectToAction("Index");
  }

  [Authorize]
  public async Task<IActionResult> Delete(int[] userId)
  {
    if (await IsUserNotExistAsync())
      return RedirectToAction("Login");
    else if (await IsUserBlockedAsync())
      return RedirectToAction("Blocked");

    foreach (int id in userId)
    {
      if (_dbContext.Users.Select(u => u.Id).Contains(id))
      {
        User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
      }
    }

    return RedirectToAction("Index");
  }

  public async Task<IActionResult> Logout()
  {
    if (HttpContext.User != null)
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction("Login");
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }

  #region Methods
  public async Task<bool> IsUserNotExistAsync()
  {
    User? loginUser = await _dbContext.Users.FirstOrDefaultAsync(u =>
      u.Name == HttpContext.User.Identity.Name);
    return loginUser == null;
  }

  public async Task<bool> IsUserBlockedAsync()
  {
    User? loginUser = await _dbContext.Users.FirstOrDefaultAsync(u =>
      u.Name == HttpContext.User.Identity.Name);
    return loginUser.UserStatus == Status.Blocked;
  }
  #endregion
}
