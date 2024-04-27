namespace Solution.Data;

public class Config
{
  public Dictionary<string, string> SupportedLanguages()
  {
    return new Dictionary<string, string>{
              {"en_US" , "English"},
              {"ru" , "Russian"},
              {"ar" , "Arabic"},
              {"ja" , "Japanese"},
              {"vi" , "Vietnamese"},
          };
  }

  public readonly int DefaultSeed = 0;
  public readonly string DefaultLocale = "en_US";
  public readonly float DefaultTypoRate = 1.5f;
}
