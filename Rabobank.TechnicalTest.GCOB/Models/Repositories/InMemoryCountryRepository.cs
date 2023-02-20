using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories;

public class InMemoryCountryRepository : InMemoryRepository<Country>, ICountryRepository
{
  public InMemoryCountryRepository(ILogger<InMemoryRepository<Country>> logger) : base(logger)
  {
    Data = new ConcurrentDictionary<int, Country>();

    Data.TryAdd(1, new Country { Id = 1, Name = "Netherlands" });
    Data.TryAdd(2, new Country { Id = 2, Name = "Poland" });
    Data.TryAdd(3, new Country { Id = 3, Name = "Ireland" });
    Data.TryAdd(4, new Country { Id = 4, Name = "South Africa" });
    Data.TryAdd(5, new Country { Id = 5, Name = "India" });
  }

  public Task<IEnumerable<Country>> GetAllAsync()
  {
    Logger.LogDebug($"Get all Countries");

    return Task.FromResult(Data.Select(x => x.Value));
  }

  protected override string EntityName => "Country";
}