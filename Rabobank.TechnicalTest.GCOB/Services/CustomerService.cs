using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Helpers;
using Rabobank.TechnicalTest.GCOB.Models.Entities;
using Rabobank.TechnicalTest.GCOB.Models.Exceptions;
using Rabobank.TechnicalTest.GCOB.Models.Repositories;
using Rabobank.TechnicalTest.GCOB.Services.Dtos;
using Rabobank.TechnicalTest.GCOB.Services.Exceptions;

namespace Rabobank.TechnicalTest.GCOB.Services;

public class CustomerService : ICustomerService
{
  private readonly ILogger<CustomerService> _logger;
  private readonly IMapper _mapper;
  private readonly ICustomerRepository _customerRepository;

  // It's probably more inline with SRP to work with these repositories in their own services. Then have a Facade implementation over the top that the controller.

  private readonly IAddressRepository _addressRepository;
  private readonly ICountryRepository _countryRepository;

  public CustomerService(ILogger<CustomerService> logger, IMapper mapper, ICustomerRepository customerRepository, IAddressRepository addressRepository, ICountryRepository countryRepository)
  {
    _logger = logger;
    _mapper = mapper;
    _customerRepository = customerRepository;
    _addressRepository = addressRepository;
    _countryRepository = countryRepository;
  }
  public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
  {
    _logger.LogDebug($"Called CustomerService to GetCustomersAsync.");

    var customers = await _customerRepository.GetAllAsync();

    var customerDtos = new List<CustomerDto>();
    foreach (var customer in customers)
    {
      var customerDto = _mapper.Map<CustomerDto>(customer);

      var address = await _addressRepository.GetAsync(customer.AddressId);
      var addressDto = _mapper.Map<AddressDto>(address);

      var country = await _countryRepository.GetAsync(address.CountryId);
      var countryDto = _mapper.Map<CountryDto>(country);
      addressDto.Country = countryDto;

      customerDto.Address = addressDto;
      customerDtos.Add(customerDto);
    }

    return customerDtos;
  }

  public async Task UpdateCustomerAsync(CustomerDto customerDto)
  {
    if (customerDto == null || customerDto.Id == 0)
      throw new Exception("Invalid customerDto. Please ensure that the customer is not null and has an Id");

    _logger.LogDebug($"Called CustomerService to update Customer. DTO data{customerDto.ToJson()}");

    try
    {
      var customer = _mapper.Map<Customer>(customerDto);
      await _customerRepository.UpdateAsync(customer);

      if (customerDto.Address == null) return;

      var address = _mapper.Map<Address>(customerDto.Address);
      await _addressRepository.UpdateAsync(address);

      // We should probably handle what to do if we have a CountryId that doesn't exist. Fail / Create.
    }
    catch (NoEntityFoundException)
    {
      throw new CustomerNotFoundException($"No customer was found to update with an identity of {customerDto.Id}");
    }
  }
}