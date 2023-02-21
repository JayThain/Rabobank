using System.Collections.Generic;
using System.Threading.Tasks;
using Rabobank.TechnicalTest.GCOB.Services.Dtos;

namespace Rabobank.TechnicalTest.GCOB.Services;

public class CustomerService : ICustomerService
{
  public IEnumerable<CustomerDto> GetCustomers()
  {
    throw new System.NotImplementedException();
  }

  public Task UpdateCustomer(CustomerDto customerDto)
  {
    throw new System.NotImplementedException();
  }

  Task<IEnumerable<CustomerDto>> ICustomerService.GetCustomers()
  {
    throw new System.NotImplementedException();
  }
}

public interface ICustomerService
{
  Task<IEnumerable<CustomerDto>> GetCustomers();
  Task UpdateCustomer(CustomerDto customerDto);
}