namespace Task3;

public class Rules
{
  readonly string userName;

  readonly string computerName;

  readonly int size;

  private readonly string[] items;

  private readonly string[,] rulesGrid;

  public Rules(string[] items, string userName, string computerName)
  {
    this.userName = userName;
    this.computerName = computerName;
    size = items.Length;
    this.items = new string[size];

    for (int i = 0; i < size; i++)
    {
      this.items[i] = items[i];
    }

    rulesGrid = new string[size, size];
    SetRules();
  }

  private void SetRules()
  {
    for (int i = 0; i < size; i++)
    {
      int n = (size - 1) / 2;
      int left = n;

      for (int j = i; j < size; j++)
      {
        if (i == j)
        {
          rulesGrid[i, j] = "Draw";
        }
        else
        {
          rulesGrid[i, j] = (size - j + i > n) ? computerName : userName;
          left--;
        }
      }

      for (int j = 0; j < i; j++)
      {
        if (left > 0)
        {
          rulesGrid[i, j] = computerName;
          left--;
        }
        else
        {
          rulesGrid[i, j] = userName;
        }
      }
    }
  }

  public string WinnerChoose(string userTurn, string computerTurn)
  {
    int x = 0;
    int y = 0;

    for (int i = 0; i < size; i++)
    {
      if (items[i] == userTurn) x = i;

      if (items[i] == computerTurn) y = i;
    }

    return rulesGrid[x, y];
  }

  public string[,] GetRules() => rulesGrid;

  public string[] GetItems() => items;
}
