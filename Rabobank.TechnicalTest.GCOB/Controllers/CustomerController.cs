using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Rabobank.TechnicalTest.GCOB.Helpers;
using Rabobank.TechnicalTest.GCOB.Services;
using Rabobank.TechnicalTest.GCOB.Services.Dtos;
using Rabobank.TechnicalTest.GCOB.Services.Exceptions;

namespace Rabobank.TechnicalTest.GCOB.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
  private readonly ILogger<CustomerController> _logger;
  private readonly ICustomerService _customerService;

  public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
  {
    _logger = logger;
    _customerService = customerService;
  }

  [HttpGet]
  public async Task<IActionResult> Get()
  {
    try
    {
      _logger.LogInformation("HTTP GET call made on Customers.");

      var customers = await _customerService.GetCustomersAsync();

      return customers.Any() ? Ok(customers) : NoContent();
    }
    catch (Exception exception)
    {
      const string error = "An error has occurred getting customers.";
      _logger.LogError(error, exception);
      return BadRequest(error);
    }
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
  {
    try
    {
      _logger.LogInformation($"HTTP POST call made to update a customer with data: {customerDto.ToJson()}");

      await _customerService.UpdateCustomerAsync(customerDto);

      return NoContent();
    }
    catch (CustomerNotFoundException)
    {
      var message = $"No customer found to update with identity {customerDto.Id}.";
      _logger.LogInformation(message);
      return NotFound(message);
    }
    catch (Exception exception)
    {
      var error = $"An error has occurred updating customer with data ${customerDto.ToJson()}.";
      _logger.LogError(error, exception);
      return BadRequest(error);
    }
  }
}