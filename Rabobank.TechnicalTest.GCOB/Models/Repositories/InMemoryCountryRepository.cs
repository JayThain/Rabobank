using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Models.Data;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories;

public class InMemoryCountryRepository : InMemoryRepository<Country>, ICountryRepository
{
  public InMemoryCountryRepository(ILogger<InMemoryRepository<Country>> logger, IEntityDataStore<Country> countryDataStore) : base(logger, countryDataStore)
  {
  }

  protected override string EntityName => "Country";
}