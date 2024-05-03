namespace Task5.Data;

  public class ChangebleField
  {
      public string Value { get; private set; }
      public string Current { get; set; }
      public ChangebleField(string value)
      {
          Value = value;
          Current = value;
      }
      public void Set(string s) => Current = s;
      public string Get() => Current;
      public int Length() => Value.Length;
      public void Update()
      {
          Current = Value;
      }
  }

  public class User
  {
      public int Id { get; private set; }
      public string Code { get; private set; }
      public ChangebleField FullName;
      public ChangebleField FullAddress;
      public ChangebleField Phone;

      public string GetFullName() => FullName.Current;
      public string GetFullAddress() => FullAddress.Current;
      public string GetPhone() => Phone.Current;
      
      public User(string fullName, string fullAddress, string phone)
      {
          FullName = new ChangebleField(fullName);
          FullAddress = new ChangebleField(fullAddress);
          Phone = new ChangebleField(phone);
          Code = "";
      }

      public List<ChangebleField> Fields => new List<ChangebleField> { FullName, FullAddress, Phone };
      public void Clear()
      {
          foreach (var field in Fields)
          {
              field.Update();
          }
      }

  }
