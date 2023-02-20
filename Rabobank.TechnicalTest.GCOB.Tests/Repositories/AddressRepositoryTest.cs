using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Tests.Repositories;

[TestClass]
public class AddressRepositoryTest
{
  private IAddressRepository _addressRepository;

  [TestInitialize]
  public void Initialize()
  {
    var logger = new Mock<ILogger<InMemoryRepository<Address>>>().Object;
    _addressRepository = new InMemoryAddressRepository(logger);
  }

  [TestMethod]
  public async Task GivenICreateACustomer_ThenICanInsertAndUpdateCustomer_CustomerIsRetrievedAndSavedCorrectly()
  {
    var address = new Address { Id = _addressRepository.GenerateIdentityAsync().Result, Street = "The Avenue", City = "Southend-on-Sea", Postcode = "CM1 1AA", CountryId = 10 };

    await _addressRepository.InsertAsync(address);

    var insertedAddress = _addressRepository.GetAsync(address.Id).Result;

    Assert.AreEqual(address.Id, insertedAddress.Id);
    Assert.AreEqual(address.Street, insertedAddress.Street);
    Assert.AreEqual(address.City, insertedAddress.City);
    Assert.AreEqual(address.CountryId, insertedAddress.CountryId);

    insertedAddress.City = "London";

    await _addressRepository.UpdateAsync(insertedAddress);

    var updatedAddress = _addressRepository.GetAsync(address.Id).Result;

    Assert.AreEqual("London", updatedAddress.City);
  }
}