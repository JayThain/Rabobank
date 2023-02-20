using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

  public Task<IEnumerable<Country>> GetAllAsync()
  {
    Logger.LogDebug($"Get all Countries");

    return Task.FromResult(Data.Select(x => x.Value));
  }

  protected override string EntityName => "Country";
}