using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Models.Dtos;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories;

public class InMemoryCustomerRepository : InMemoryRepository<Customer>, ICustomerRepository
{
  public InMemoryCustomerRepository(ILogger<InMemoryRepository<Customer>> logger) : base(logger)
  {
    Data = new ConcurrentDictionary<int, Customer>();
  }

  protected override string EntityName => "Customer";
}