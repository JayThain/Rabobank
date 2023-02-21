using AutoMapper;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Services.Dtos;

namespace Rabobank.TechnicalTest.GCOB.Configuration
{
  public class Maps : Profile
  {
    public Maps()
    {
      CreateMap<CustomerDto, Customer>();
      CreateMap<AddressDto, Address>();
      CreateMap<CountryDto, Country>();

      CreateMap<Customer, CustomerDto>();
      CreateMap<Address, AddressDto>();
      CreateMap<Country, CountryDto>();
    }
  }
}
