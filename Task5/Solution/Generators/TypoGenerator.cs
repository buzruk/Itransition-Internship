namespace Solution.Generators;

delegate string TypoType(string s);
public class TypoGenerator
{
  public float MaxLengthDiff { get; private set; } = 10;
  public float TypoRate { get; set; }
  public Faker Faker { get; set; }
  public TypoGenerator(float typoRate = 1.5f, string locale = "en_US")
  {
    TypoRate = typoRate;
    Faker = new Faker(locale);
  }

  public IEnumerable<User> ChangeUsers(IEnumerable<User> users)
  {
    Randomizer.Seed = new Random(0);

    foreach (var user in users)
    {
      user.Clear();
      ChangeUser(user);
    }

    return users;
  }

  private void ChangeUser(User user)
  {
    int Typos = (int)Math.Truncate(TypoRate);
    for (int i = 0; i < Typos; i++)
    {
      MakeTypo(user);
    }

    float PossibleTypo = TypoRate - Typos;
    if (PossibleTypo != 0 && PossibleTypo > Faker.Random.Float())
    {
      MakeTypo(user);
    }
  }

  private void MakeTypo(User user)
  {
    var field = Faker.PickRandom(user.Fields); //Get Getters(Key) and Setters(Value)
    string s = field.Get();
    field.Set(MakeTypo(s, field.Length()));
  }

  private string MakeTypo(string str, int startedLength)
  {
    List<TypoType> typos = new List<TypoType>() { SwapChars };
    if (str.Length > 5 && startedLength - str.Length < MaxLengthDiff)
    {
      typos.Add(DeleteRandomChar);
    }

    if (str.Length - startedLength < MaxLengthDiff)
    {
      typos.Add(AddRandomChar);
    }

    return Faker.PickRandom(typos)(str);
  }

  private string DeleteRandomChar(string s)
  {
    if (string.IsNullOrEmpty(s)) return s;

    return s.Remove(Faker.Random.Int(0, s.Length - 1), 1);
  }

  private string AddRandomChar(string s)
  {
    if (s == null) s = "";

    string letter = Faker.Lorem.Letter();
    if (Faker.Random.Bool())
    {
      letter = letter.ToUpper();
    }

    return s.Insert(Faker.Random.Int(0, s.Length), letter);
  }

  private string SwapChars(string s)
  {
    if (string.IsNullOrWhiteSpace(s) || s.Length < 2) return s;

    int first = Faker.Random.Int(0, s.Length - 1);
    int second = Faker.Random.Int(0, s.Length - 1);
    if (second == first)
    {
      second = first == s.Length - 1 ? first - 1 : first + 1;
    }

    return Swap(s, first, second);
  }

  private string Swap(string s, int firstId, int secondId)
  {
    if (firstId < 0 || firstId >= s.Length || secondId < 0 || secondId >= s.Length) return s;

    string first = s[firstId].ToString();
    string second = s[secondId].ToString();
    s = s.Remove(firstId, 1).Insert(firstId, second);
    return s.Remove(secondId, 1).Insert(secondId, first);
  }
}
