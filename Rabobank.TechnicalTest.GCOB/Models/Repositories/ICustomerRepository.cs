using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories;

// Maybe you don't need these blank interfaces. However in the real world you would probably have some methods specific to each repository.

public interface ICustomerRepository: IRepository<Customer>
{
}