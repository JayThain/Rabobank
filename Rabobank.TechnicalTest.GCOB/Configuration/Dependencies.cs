using Microsoft.Extensions.DependencyInjection;
using Rabobank.TechnicalTest.GCOB.Models.Data;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Repositories;
using Rabobank.TechnicalTest.GCOB.Services;

namespace Rabobank.TechnicalTest.GCOB.Configuration;

public static class Dependencies
{
  internal static void ConfigureServices(IServiceCollection services)
  {
    services.AddSingleton<IEntityDataStore<Customer>>();
    services.AddSingleton<IEntityDataStore<Address>>();
    services.AddSingleton<IEntityDataStore<Country>>();
    services.AddSingleton<CountryDataStoreInitializer>();
    
    services.AddScoped<ICustomerRepository, InMemoryCustomerRepository>();
    services.AddScoped<IAddressRepository, InMemoryAddressRepository>();
    services.AddScoped<ICountryRepository, InMemoryCountryRepository>();

    services.AddScoped<ICustomerService, CustomerService>();
  }
}


