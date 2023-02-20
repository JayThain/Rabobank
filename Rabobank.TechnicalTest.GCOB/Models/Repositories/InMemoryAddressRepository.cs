using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Models.Data;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories;

public class InMemoryAddressRepository : InMemoryRepository<Address>, IAddressRepository
{
  public InMemoryAddressRepository(ILogger<InMemoryRepository<Address>> logger, IEntityDataStore<Address> addressDataStore) : base(logger, addressDataStore)
  {
  }

  protected override string EntityName => "Address";
}