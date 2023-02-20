using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Tests.Repositories
{
  [TestClass]
  public class CustomerRepositoryTest
  {
    private ICustomerRepository _customerRepository;

    [TestInitialize]
    public void Initialize()
    {
      var logger = new Mock<ILogger<InMemoryRepository<Customer>>>().Object;
      _customerRepository = new InMemoryCustomerRepository(logger);
    }
    
    [TestMethod]
    public async Task GivenICreateACustomer_ThenICanInsertAndUpdateCustomer_CustomerIsRetrievedAndSavedCorrectly()
    {
      var customer = new Customer { Id = _customerRepository.GenerateIdentityAsync().Result, FirstName = "John", LastName = "Smith", AddressId = 10};

      await _customerRepository.InsertAsync(customer);

      var insertedCustomer = _customerRepository.GetAsync(customer.Id).Result;

      Assert.AreEqual(customer.Id, insertedCustomer.Id);
      Assert.AreEqual(customer.FirstName, insertedCustomer.FirstName);
      Assert.AreEqual(customer.LastName, insertedCustomer.LastName);
      Assert.AreEqual(customer.AddressId, insertedCustomer.AddressId);

      insertedCustomer.FirstName = "Paul";

      await _customerRepository.UpdateAsync(insertedCustomer);

      var updatedCustomer = _customerRepository.GetAsync(customer.Id).Result;

      Assert.AreEqual("Paul", updatedCustomer.FirstName);
    }
  }
}
