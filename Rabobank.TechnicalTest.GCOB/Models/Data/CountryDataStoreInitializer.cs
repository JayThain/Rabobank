using Rabobank.TechnicalTest.GCOB.Models.Entities;

namespace Rabobank.TechnicalTest.GCOB.Models.Data;

public class CountryDataStoreInitializer
{
  public CountryDataStoreInitializer()
  {
    var dataStore = EntityDataStore<Country>.Instance;

    dataStore.Data.TryAdd(1, new Country { Id = 2, Name = "Poland" });
    dataStore.Data.TryAdd(2, new Country { Id = 3, Name = "Ireland" });
    dataStore.Data.TryAdd(3, new Country { Id = 4, Name = "South Africa" });
    dataStore.Data.TryAdd(4, new Country { Id = 1, Name = "Netherlands" });
    dataStore.Data.TryAdd(5, new Country { Id = 5, Name = "India" });
  }

}