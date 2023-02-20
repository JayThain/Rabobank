using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Models.Data;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories;

public class InMemoryCustomerRepository : InMemoryRepository<Customer>, ICustomerRepository
{
  public InMemoryCustomerRepository(ILogger<InMemoryRepository<Customer>> logger, IEntityDataStore<Customer> customerDataStore) : base(logger, customerDataStore)
  {
  }

  protected override string EntityName => "Customer";
}