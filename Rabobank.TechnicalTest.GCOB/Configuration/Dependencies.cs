using Microsoft.Extensions.DependencyInjection;
using Rabobank.TechnicalTest.GCOB.Models.Repositories;

namespace Rabobank.TechnicalTest.GCOB.Configuration;

public static class Dependencies
{
  internal static void ConfigureServices(IServiceCollection services)
  {
    services.AddScoped<ICustomerRepository, InMemoryCustomerRepository>();
    services.AddScoped<IAddressRepository, InMemoryAddressRepository>();
    services.AddScoped<ICountryRepository, InMemoryCountryRepository>();
  }
}


