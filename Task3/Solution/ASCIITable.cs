using ConsoleTableExt;
using System.Collections.Generic;
using System.Linq;

namespace Task3;

static class ASCIITable
{

  public static void CreateTable(Rules rules)
  {
    var names = rules.GetItems();

    var grid = rules.GetRules();

    int len = names.Length;

    var list = new List<List<object>>();

    var topic = names.Cast<object>().ToList();

    topic.Insert(0, @" v User\Computer >");

    list.Add(topic);

    for (int i = 0; i < names.Length; i++)
    {
      object[] row = Enumerable.Range(0, len)
                               .Select(colNum => (object)grid[i, colNum])
                               .ToArray();

      var listCol = row.Cast<object>().ToList();

      listCol.Insert(0, names[i]);

      list.Add(listCol);
    }

    ConsoleTableBuilder.From(list)
                       .WithFormat(ConsoleTableBuilderFormat.Alternative)
                       .ExportAndWriteLine();
  }
}
