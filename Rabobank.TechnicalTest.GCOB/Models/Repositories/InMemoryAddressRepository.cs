using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories;

public class InMemoryAddressRepository : InMemoryRepository<Address>, IAddressRepository
{
  public InMemoryAddressRepository(ILogger<InMemoryRepository<Address>> logger) : base(logger)
  {
    Data = new ConcurrentDictionary<int, Address>();
  }

  protected override string EntityName => "Address";
}