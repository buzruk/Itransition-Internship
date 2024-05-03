using Bogus;
using Task5.Data;

namespace Task5.Generators;

  delegate string AddressType();
  public class UserGenerator
  {
      public string Locale { get; private set; }
      private Faker Faker { get; set; }
      private Faker<User> UserFaker;
      public UserGenerator(string locale = "en_US")
      {
          Locale = locale;
          Faker = new Faker(locale);
          UserFaker = CreateUserFaker(1);
      }

      public IEnumerable<User> GeneratePage(int count = 10)
      {
          return GenerateUsers().Take(count);
      }

      public IEnumerable<User> GenerateUsers()
      {
          return UserFaker.GenerateForever();
      }

      private Faker<User> CreateUserFaker(int id)
      {
          return new Faker<User>(Locale)
              .CustomInstantiator(f => new User(f.Name.FullName(), this.Address(), f.Phone.PhoneNumberFormat()))
              .RuleFor(u => u.Id, f => id++)
              .RuleFor(u => u.Code, f => f.Random.Hash(10));
      }

      private string Address()
      {
          return string.Join(", ", GetAddress().Select(x => x()));
      }

      private List<AddressType> GetAddress()
      {
          var addresses = GetAddreses();
          return Faker.PickRandom(addresses);
      }

      private List<List<AddressType>> GetAddreses()
      {
          return new List<List<AddressType>>()
          {
              new List<AddressType>(){Faker.Address.City, Faker.Address.StreetName, Faker.Address.BuildingNumber, Faker.Address.SecondaryAddress},
              new List<AddressType>(){Faker.Address.Country, Faker.Address.City, Faker.Address.StreetName, Faker.Address.BuildingNumber},
              new List<AddressType>(){Faker.Address.State, Faker.Address.City, Faker.Address.StreetName, Faker.Address.BuildingNumber},
              new List<AddressType>(){Faker.Address.FullAddress},
          };
      }
  }
