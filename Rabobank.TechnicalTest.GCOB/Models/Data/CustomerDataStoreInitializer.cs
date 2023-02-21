using Rabobank.TechnicalTest.GCOB.Models.Entities;

namespace Rabobank.TechnicalTest.GCOB.Models.Data;

public class CustomerDataStoreInitializer
{
  public CustomerDataStoreInitializer()
  {
    var dataStore = EntityDataStore<Customer>.Instance;

    dataStore.Data.TryAdd(1, new Customer { Id = 1, FirstName = "Paul", LastName = "Simon", AddressId = 1 });
    dataStore.Data.TryAdd(2, new Customer { Id = 2, FirstName = "John", LastName = "Lennon", AddressId = 2 });
    dataStore.Data.TryAdd(3, new Customer { Id = 3, FirstName = "Peter", LastName = "Hill", AddressId = 3 });
  }
}