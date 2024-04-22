using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Task3;

class Program
{
  static void Main(string[] items)
  {
    while (true)
    {
      if (!IsValid(items)) break;

      if (PerformAction(items)) continue;
      else break;
    }
  }

  static bool PerformAction(string[] items)
  {
    bool IsShowingNext = true;

    Rules rules = new(items, "User", "Computer");

    string currentComputerTurn = RandomComputerTurn(items);

    string currentPCKey = KeyAndHMACGenerator.KeyGenerator();

    string currentHMAC = KeyAndHMACGenerator.HMACGenerator(currentPCKey, currentComputerTurn);

    PrintSeparator();
    Console.WriteLine($"Generated HMAC: {currentHMAC}");

    string menuOption = Menu(items);

    if (menuOption == "?")
    {
      ASCIITable.CreateTable(rules);
    }
    else if (menuOption == "0")
    {
      IsShowingNext = false;
      return IsShowingNext;
    }
    else
    {
      string currentUserTurn = UserTurn(items, menuOption);
      Console.WriteLine($"Your move: {currentUserTurn}");
      Console.WriteLine($"Computer move: {currentComputerTurn}");
      Console.WriteLine("----------------------------");
      string gameResult = rules.WinnerChoose(currentUserTurn, currentComputerTurn);


      var fullResult = gameResult == "Draw" ? "Draw" : $"{gameResult} Win";
      Show(State.Success, fullResult);

      Console.WriteLine();
      PrintSeparator();
      Console.WriteLine($"Original KEY: {currentPCKey}\n\n");
      PrintSeparator();
    }
    return IsShowingNext;
  }

  static bool IsValid(string[] items)
  {
    bool result = false;

    if (items.Length < 3)
    {
      Show(State.Error, "Use at least 3 items");
      return result;
    }

    if ((items.Length % 2) != 1)
    {
      Show(State.Error, "Use an odd number of items");
      return result;
    }

    HashSet<string> uniqueMoves = [];
    foreach (string move in items)
    {
      if (uniqueMoves.Contains(move))
      {
        Show(State.Error, $"Error: The move {move} is repeated. Please provide non-repeating move");
        return result;
      }
      uniqueMoves.Add(move);
    }

    return true;
  }

  static string Menu(string[] items)
  {
    while (true)
    {
      PrintSeparator();
      for (int i = 0; i < items.Length; i++)
      {
        Console.WriteLine($"{i + 1} - {items[i]}");
      }
      Console.WriteLine("0 - Exit");
      Console.WriteLine("? - Help");
      Console.Write("Please, enter your move: ");
      string option = Console.ReadLine();
      PrintSeparator();

      if (option.ToLower() == "exit")
      {
        Show(State.Error, "Enter 0 to stop the program");
        continue;
      }

      if (option.ToLower() == "help")
      {
        Show(State.Error, "For help enter ?");
        continue;
      }

      if (option == "?")
      {
        return option;
      }
      else if (int.TryParse(option, out int temp))
      {
        int convertedOption = Convert.ToInt32(option);
        if (convertedOption < 0)
        {
          Show(State.Error, "Your choice must be greater than or equal 0");
          continue;
        }
        else if (convertedOption > items.Length)
        {
          Show(State.Error, $"Your choice must be less than {items.Length}");
          continue;
        }
        else
        {
          return convertedOption.ToString();
        }
      }
      else
      {
        continue;
      }
    }
  }

  enum State
  {
    Error,
    Success
  }

  static void Show(State state, string message)
  {
    if (state == State.Error)
      Console.BackgroundColor = ConsoleColor.Red;
    else
      Console.BackgroundColor = ConsoleColor.Green;

    Console.WriteLine(message);
    Console.ResetColor();
  }

  static void PrintSeparator() => Console.WriteLine("============================");

  static string RandomComputerTurn(string[] arrTurns)
  {
    int result = RandomNumberGenerator.GetInt32(arrTurns.Length);
    return arrTurns[result];
  }

  static string UserTurn(string[] arrTurns, string option)
    => arrTurns[Convert.ToInt32(option) - 1];
}
