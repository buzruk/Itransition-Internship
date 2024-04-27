namespace Solution.Data;

public class UserClassMap : ClassMap<User>
{
  public UserClassMap()
  {
    Map(u => u.Id).Name("Id");
    Map(u => u.Code).Name("Code");
    Map(u => u.FullName).Name("Name").Convert(user =>
    {
      return user.Value.GetFullName();
    });
    Map(u => u.FullAddress).Name("Address").Convert(user =>
    {
      return user.Value.GetFullAddress();
    });
    Map(u => u.Phone).Name("Phone").Convert(user =>
    {
      return user.Value.GetPhone();
    });
  }
}

public class Export
{
  public MemoryStream GetStream(IEnumerable<User> data)
  {
    using (var ms = new MemoryStream())
    {
      using (var wr = new StreamWriter(ms))
      using (var csvWriter = new CsvWriter(wr, CultureInfo.InvariantCulture))
      {
        csvWriter.Context.RegisterClassMap<UserClassMap>();
        csvWriter.WriteRecords(data);
      }
      return ms;
    }
  }
}
