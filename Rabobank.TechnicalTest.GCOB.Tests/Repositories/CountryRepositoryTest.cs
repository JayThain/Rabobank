using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Tests.Repositories;

[TestClass]
public class CountryRepositoryTest
{
  private ICountryRepository _countryRepository;

  [TestInitialize]
  public void Initialize()
  {
    var logger = new Mock<ILogger<InMemoryRepository<Country>>>().Object;
    _countryRepository = new InMemoryCountryRepository(logger);
  }

  [TestMethod]
  public async Task GivenICreateACustomer_ThenICanInsertAndUpdateCustomer_CustomerIsRetrievedAndSavedCorrectly()
  {
    var country = new Country { Id = _countryRepository.GenerateIdentityAsync().Result, Name = "Great Britain" };

    await _countryRepository.InsertAsync(country);

    var insertCountry = _countryRepository.GetAsync(country.Id).Result;

    Assert.AreEqual(country.Id, insertCountry.Id);
    Assert.AreEqual(country.Name, insertCountry.Name);

    insertCountry.Name = "Canada";

    await _countryRepository.UpdateAsync(insertCountry);

    var updatedCountry = _countryRepository.GetAsync(country.Id).Result;

    Assert.AreEqual("Canada", updatedCountry.Name);
  }

  [TestMethod]
  public async Task GivenIHavePopulatedCountryData_ThenGetAll_Returns5Items()
  {
    var countries = await _countryRepository.GetAllAsync();

    Assert.AreEqual(5, countries.Count());
  }
}