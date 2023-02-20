using Microsoft.Extensions.DependencyInjection;
using Rabobank.TechnicalTest.GCOB.Models.Repositories;

namespace Rabobank.TechnicalTest.GCOB.Configuration;

public static class Dependencies
{
  internal static void ConfigureServices(IServiceCollection services)
  {
    services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
    services.AddSingleton<IAddressRepository, InMemoryAddressRepository>();
    services.AddSingleton<ICountryRepository, InMemoryCountryRepository>();
  }
}


