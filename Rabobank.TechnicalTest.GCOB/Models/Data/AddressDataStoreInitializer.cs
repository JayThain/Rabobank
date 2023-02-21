using Rabobank.TechnicalTest.GCOB.Models.Entities;

namespace Rabobank.TechnicalTest.GCOB.Models.Data;

public class AddressDataStoreInitializer
{
  public AddressDataStoreInitializer()
  {
    var dataStore = EntityDataStore<Address>.Instance;

    dataStore.Data.TryAdd(1, new Address { Id = 1, Street = "The Avenue", City = "Southend-on-Sea", Postcode= "SS1 1AA", CountryId = 1 });
    dataStore.Data.TryAdd(2, new Address { Id = 2, Street = "North Street", City = "Canterbury", Postcode = "C1, 1BB", CountryId = 2 });
    dataStore.Data.TryAdd(3, new Address { Id = 3, Street = "Central Avenue", City = "London", Postcode="N1 1CC", CountryId = 3 });
  }
}