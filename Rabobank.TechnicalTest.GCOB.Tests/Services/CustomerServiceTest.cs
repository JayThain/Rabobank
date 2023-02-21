using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Services;
using Moq;
using Rabobank.TechnicalTest.GCOB.Configuration;
using Rabobank.TechnicalTest.GCOB.Models.Data;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories;
using Rabobank.TechnicalTest.GCOB.Services.Dtos;

namespace Rabobank.TechnicalTest.GCOB.Tests.Services;

[TestClass]
public class CustomerServiceTest
{
  private ILogger<CustomerService> _logger;
  private IMapper _mapper;

  [TestInitialize]
  public void Initialize()
  {
    _logger = new Mock<ILogger<CustomerService>>().Object;
    _mapper = GetMapper();
  }

  [TestMethod] 
  public async Task WhenICallGetCustomers_ThenAllCustomers_ShouldBeReturnedInCustomerDtos()
  {
    var customerDataStore = EntityDataStore<Customer>.Instance;
    customerDataStore.Data.Clear();
    customerDataStore.Data.TryAdd(1, new Customer { Id = 1, FirstName = "Paul", LastName = "Simon", AddressId = 1 });
    customerDataStore.Data.TryAdd(2, new Customer { Id = 2, FirstName = "John", LastName = "Lennon", AddressId = 2 });

    var addressDataStore = EntityDataStore<Address>.Instance;
    addressDataStore.Data.Clear();
    addressDataStore.Data.TryAdd(1, new Address { Id = 1, Street = "The Avenue", City = "Southend-on-Sea", Postcode = "SS1 1AA", CountryId = 1 });
    addressDataStore.Data.TryAdd(2, new Address { Id = 2, Street = "North Street", City = "Canterbury", Postcode = "C1 1BB", CountryId = 2 });

    var countryDataStore = EntityDataStore<Country>.Instance;
    countryDataStore.Data.Clear();
    countryDataStore.Data.TryAdd(1, new Country { Id = 1, Name = "Poland" });
    countryDataStore.Data.TryAdd(2, new Country { Id = 2, Name = "Ireland" });

    var customerDtos = await GetCustomerService().GetCustomersAsync();

    customerDtos = customerDtos.ToArray();

    var customerDto1 = customerDtos.Single(x => x.Id == 1);
    Assert.AreEqual("Paul", customerDto1.FirstName);
    Assert.AreEqual("Simon", customerDto1.LastName);
    Assert.AreEqual("The Avenue", customerDto1.Address.Street);
    Assert.AreEqual("Southend-on-Sea", customerDto1.Address.City);
    Assert.AreEqual("SS1 1AA", customerDto1.Address.Postcode);
    Assert.AreEqual("Poland", customerDto1.Address.Country.Name);

    var customerDto2 = customerDtos.Single(x => x.Id == 2);
    Assert.AreEqual("John", customerDto2.FirstName);
    Assert.AreEqual("Lennon", customerDto2.LastName);
    Assert.AreEqual("North Street", customerDto2.Address.Street);
    Assert.AreEqual("Canterbury", customerDto2.Address.City);
    Assert.AreEqual("C1 1BB", customerDto2.Address.Postcode);
    Assert.AreEqual("Ireland", customerDto2.Address.Country.Name);
  }

  [TestMethod]
  public async Task WhenICallGetCustomersWithNoData_ThenAnEmptyCollectionReturned()
  {
    EntityDataStore<Customer>.Instance.Data.Clear();
    EntityDataStore<Address>.Instance.Data.Clear();
    EntityDataStore<Country>.Instance.Data.Clear();

    var customerDtos = await GetCustomerService().GetCustomersAsync();

    Assert.IsNotNull(customerDtos);
    Assert.IsTrue(!customerDtos.Any());
  }

  [TestMethod]
  public async Task WhenIUpdateACustomerDto_ThenTheDataStoreShouldBeUpdated()
  {
    var customerDataStore = EntityDataStore<Customer>.Instance;
    customerDataStore.Data.Clear();
    customerDataStore.Data.TryAdd(1, new Customer { Id = 1, FirstName = "Paul", LastName = "Simon", AddressId = 1 });

    var addressDataStore = EntityDataStore<Address>.Instance;
    addressDataStore.Data.Clear();
    addressDataStore.Data.TryAdd(1, new Address { Id = 1, Street = "The Avenue", City = "Southend-on-Sea", Postcode = "SS1 1AA", CountryId = 0 });

    var customerService = GetCustomerService();
    await customerService.UpdateCustomerAsync(new CustomerDto
    {
      Id = 1,
      FirstName = "Paul Updated",
      LastName = "Simon Updated",
      Address = new AddressDto
      {
        Id = 1, Street = "The Avenue Updated", City = "Southend-on-Sea Updated", Postcode = "SS1 1AA Updated"
      }
    });

    Assert.AreEqual("Paul Updated", customerDataStore.Data[1].FirstName);
    Assert.AreEqual("Simon Updated", customerDataStore.Data[1].LastName);
    Assert.AreEqual("The Avenue Updated", addressDataStore.Data[1].Street);
    Assert.AreEqual("Southend-on-Sea Updated", addressDataStore.Data[1].City);
    Assert.AreEqual("SS1 1AA Updated", addressDataStore.Data[1].Postcode);

  }

  private ICustomerService GetCustomerService()
  {
    var customerRepository = new InMemoryCustomerRepository(new Mock<ILogger<InMemoryCustomerRepository>>().Object, EntityDataStore<Customer>.Instance);
    var addressRepository = new InMemoryAddressRepository(new Mock<ILogger<InMemoryAddressRepository>>().Object, EntityDataStore<Address>.Instance);
    var countryRepository = new InMemoryCountryRepository(new Mock<ILogger<InMemoryCountryRepository>>().Object, EntityDataStore<Country>.Instance);

    var customerService = new CustomerService(new Mock<ILogger<CustomerService>>().Object, _mapper, customerRepository, addressRepository, countryRepository);

    return customerService;
  }

  private static IMapper GetMapper()
  {
    var configuration = new MapperConfiguration(cfg => {
      cfg.AddProfile<Maps>();
    });
    return new Mapper(configuration);
  }
}