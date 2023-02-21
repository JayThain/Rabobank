using System.Collections.Generic;
using System.Threading.Tasks;
using Rabobank.TechnicalTest.GCOB.Services.Dtos;

namespace Rabobank.TechnicalTest.GCOB.Services;

public interface ICustomerService
{
  Task<IEnumerable<CustomerDto>> GetCustomersAsync();
  Task UpdateCustomerAsync(CustomerDto customerDto);
}