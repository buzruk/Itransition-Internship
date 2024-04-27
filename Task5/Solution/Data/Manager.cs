namespace Solution.Data;

public class Manager
{
  public UserGenerator UserGenerator;
  public TypoGenerator TypoGenerator;
  public string Locale { get; set; }
  public List<User> users = new List<User>();
  public Manager(int seed = 0, string locale = "en_US", float rate = 1f)
  {
    Randomizer.Seed = new Random(seed);
    UserGenerator = new UserGenerator(locale);
    TypoGenerator = new TypoGenerator(rate, locale);
    Locale = locale;
    AddUsers(20);
  }

  public void UpdateTypoRate(float newRate)
  {
    TypoGenerator = new TypoGenerator(newRate, Locale);
  }

  public List<User> DirtyUsers()
  {
    return TypoGenerator.ChangeUsers(users).ToList();
  }

  public void AddUsers(int count = 10)
  {
    users.AddRange(UserGenerator.GeneratePage(count));
  }
}
