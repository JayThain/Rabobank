using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Controllers;
using Rabobank.TechnicalTest.GCOB.Services;
using Rabobank.TechnicalTest.GCOB.Services.Dtos;
using Rabobank.TechnicalTest.GCOB.Services.Exceptions;

namespace Rabobank.TechnicalTest.GCOB.Tests.Controllers;

[TestClass]
public class CustomerControllerTest
{
  ILogger<CustomerController> _logger;

  [TestInitialize]
  public void Initialize()
  {
    _logger = new Mock<ILogger<CustomerController>>().Object;
  }

  [TestMethod]
  public async Task WhenIGetAnEmptyListOfCustomers_ThenIShouldSeeANoContentResult()
  {
    var customerServiceMock = MockCustomerServiceGetCustomers(Array.Empty<CustomerDto>());
    var customerController = new CustomerController(_logger, customerServiceMock.Object);
    var result = await customerController.Get();
    var noContentResult = result as NoContentResult;
    Assert.IsNotNull(noContentResult);
    Assert.AreEqual(204, noContentResult.StatusCode);
  }

  [TestMethod]
  public async Task WhenIGetAListOfCustomers_ThenIShouldSeeAnOkResult()
  {
    var customers = new CustomerDto[] { new() { Id = 1, FirstName = "a" }, new() { Id = 2, FirstName = "b" } };
    var customerServiceMock = MockCustomerServiceGetCustomers(customers);
    var customerController = new CustomerController(_logger, customerServiceMock.Object);
    var result = await customerController.Get();
    var okResult = result as OkObjectResult;
    Assert.IsNotNull(okResult);
    Assert.AreEqual(200, okResult.StatusCode);
  }

  [TestMethod]
  public async Task WhenIUpdateACustomerCorrectly_ThenIShouldSeeANoContentOkResult()
  {
    var customerServiceMock = MockCustomerServiceUpdateCustomer(DoNothing);
    var customerController = new CustomerController(_logger, customerServiceMock.Object);
    var result = await customerController.Post(new CustomerDto());
    var okResult = result as NoContentResult;
    Assert.IsNotNull(okResult);
    Assert.AreEqual(204, okResult.StatusCode);
  }

  [TestMethod]
  public async Task WhenIUpdateACustomerThatDoesNotExist_ThenIShouldSeeANotFoundResult()
  {
    var customerServiceMock = MockCustomerServiceUpdateCustomer(() => throw new CustomerNotFoundException("Anything"));
    var customerController = new CustomerController(_logger, customerServiceMock.Object);
    var result = await customerController.Post(new CustomerDto());
    var notFoundResult = result as NotFoundObjectResult;
    Assert.IsNotNull(notFoundResult);
    Assert.AreEqual(404, notFoundResult.StatusCode);
  }

  [TestMethod]
  public async Task WhenIUpdateACustomerAndThereIsAnUnexpectedError_ThenIShouldSeeAnErrorResult()
  {
    var customerServiceMock = MockCustomerServiceUpdateCustomer(() => throw new Exception("Error"));
    var customerController = new CustomerController(_logger, customerServiceMock.Object);
    var result = await customerController.Post(new CustomerDto());
    var errorResult = result as BadRequestObjectResult;
    Assert.IsNotNull(errorResult);
    Assert.AreEqual(400, errorResult.StatusCode);
  }
  private static Mock<ICustomerService> MockCustomerServiceGetCustomers(IEnumerable<CustomerDto> customers)
  {
    var mock = new Mock<ICustomerService>();
    mock.Setup(x => x.GetCustomersAsync()).Returns(
      () => Task.FromResult(customers));
    return mock;
  }

  private static Mock<ICustomerService> MockCustomerServiceUpdateCustomer(Func<Task> updateFunc)
  {
    var mock = new Mock<ICustomerService>();
    mock.Setup(x => x.UpdateCustomerAsync(It.IsAny<CustomerDto>())).Returns(Task.Run(updateFunc));
    return mock;
  }

  private static Task DoNothing()
  {
    return Task.Run(() => { });
  }
}