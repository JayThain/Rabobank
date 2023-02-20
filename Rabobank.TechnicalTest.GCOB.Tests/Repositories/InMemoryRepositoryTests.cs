using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract;
using Rabobank.TechnicalTest.GCOB.Models.Exceptions;
using Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Tests.Repositories;

[TestClass]
public class InMemoryRepositoryTests
{
  private ILogger<InMemoryRepository<EntityStub>> _logger;

  [TestInitialize]
  public void TestInitialize()
  {
    _logger = new Mock<ILogger<InMemoryRepository<EntityStub>>>().Object;
  }

  // Testing ID Generation
  [TestMethod]
  public void GivenIHave0ItemsOfData_ThenNextID_ShouldBe1()
  {
    var repository = new InMemoryRepositoryStub0(_logger);
    Assert.AreEqual(1, repository.GenerateIdentityAsync().Result);
  }

  [TestMethod]
  public void GivenIHave1ItemsOfData_ThenNextID_ShouldBe2()
  {
    var repository = new InMemoryRepositoryStub1(_logger);
    Assert.AreEqual(2, repository.GenerateIdentityAsync().Result);
  }

  [TestMethod]
  public void GivenIHave5ItemsOfData_ThenNextID_ShouldBe6()
  {
    var repository = new InMemoryRepositoryStub5(_logger);
    Assert.AreEqual(6, repository.GenerateIdentityAsync().Result);
  }

  [TestMethod]
  public void GivenIHaveNotInitializedTheData_ThenExceptionShouldBeThrown()
  {
    var repository = new InMemoryRepositoryStubError(_logger);
    Assert.ThrowsExceptionAsync<InvalidRepositoryConfigurationException>(repository.GenerateIdentityAsync);
  }

  // Testing GetAsync
  [TestMethod]
  public void GivenIHave0ItemsOfData_ThenIShouldSeeANotFoundException()
  {
    var repository = new InMemoryRepositoryStub0(_logger);
    Assert.ThrowsExceptionAsync<NoEntityFoundException>(() => repository.GetAsync(1));
  }

  [TestMethod]
  public void GivenIHave1ItemOfData_ThenIShouldFindThatItem()
  {
    var repository = new InMemoryRepositoryStub1(_logger);
    var entity = repository.GetAsync(1);
    Assert.AreEqual("x", entity.Result.SomeField);
  }

  [TestMethod]
  public void GivenIHave5ItemOfData_ThenIShouldFindASpecificItem()
  {
    var repository = new InMemoryRepositoryStub5(_logger);
    var entity = repository.GetAsync(3);
    Assert.AreEqual("c", entity.Result.SomeField);
  }

  // Testing InsertAsync
  [TestMethod]
  public void GivenIHave0ItemOfData_ThenIShouldBeAbleToInsertAnItem()
  {
    var repository = new InMemoryRepositoryStub0(_logger);
    var entity = new EntityStub { Id = repository.GenerateIdentityAsync().Result, SomeField = "x" };
    repository.InsertAsync(entity);

    Assert.AreEqual(2, repository.GenerateIdentityAsync().Result);
    Assert.AreEqual("x", repository.GetAsync(entity.Id).Result.SomeField);
  }

  [TestMethod]
  public void GivenIHave1ItemOfData_ThenIShouldNotBeAbleToInsertAnItemWithSameId()
  {
    var repository = new InMemoryRepositoryStub1(_logger);
    var entity = new EntityStub { Id = 1, SomeField = "x" };
    Assert.ThrowsExceptionAsync<DuplicateEntityFoundException>(() => repository.InsertAsync(entity));
  }

  // Testing UpdateAsync
  [TestMethod]
  public void GivenIHave1ItemOfData_ThenIShouldBeAbleToUpdateThatItem()
  {
    var repository = new InMemoryRepositoryStub1(_logger);
    
    var entity = repository.GetAsync(1).Result;

    entity.SomeField = "new value";
    repository.UpdateAsync(entity);

    Assert.AreEqual(entity.SomeField, repository.GetAsync(1).Result.SomeField);
  }

  [TestMethod]
  public void GivenIHave1ItemOfData_ThenIShouldNotBeAbleToUpdateTheItemIfItDoenstExist()
  {
    var repository = new InMemoryRepositoryStub1(_logger);

    var entity = new EntityStub { Id = 10, SomeField = "Anything" };

    Assert.ThrowsExceptionAsync<NoEntityFoundException>(() => repository.UpdateAsync(entity));
  }


  // Test repository implementations with 0, 1 and 5 items in their data sets.
  public class InMemoryRepositoryStub0 : InMemoryRepository<EntityStub>
  {
    public InMemoryRepositoryStub0(ILogger<InMemoryRepository<EntityStub>> logger) : base(logger)
    {
      Data = new ConcurrentDictionary<int, EntityStub>();
    }

    protected override string EntityName => "EntityStub";
  }
  public class InMemoryRepositoryStub1 : InMemoryRepository<EntityStub>
  {
    public InMemoryRepositoryStub1(ILogger<InMemoryRepository<EntityStub>> logger) : base(logger)
    {
      Data = new ConcurrentDictionary<int, EntityStub>();
      Data.TryAdd(1, new EntityStub { Id = 1, SomeField = "x" });
    }

    protected override string EntityName => "EntityStub";
  }
  public class InMemoryRepositoryStub5 : InMemoryRepository<EntityStub>
  {
    public InMemoryRepositoryStub5(ILogger<InMemoryRepository<EntityStub>> logger) : base(logger)
    {
      Data = new ConcurrentDictionary<int, EntityStub>();
      Data.TryAdd(1, new EntityStub { Id = 1, SomeField = "a" });
      Data.TryAdd(2, new EntityStub { Id = 2, SomeField = "b" });
      Data.TryAdd(3, new EntityStub { Id = 3, SomeField = "c" });
      Data.TryAdd(4, new EntityStub { Id = 4, SomeField = "d" });
      Data.TryAdd(5, new EntityStub { Id = 5, SomeField = "e" });
    }

    protected override string EntityName => "EntityStub";
  }
  public class InMemoryRepositoryStubError : InMemoryRepository<EntityStub>
  {
    public InMemoryRepositoryStubError(ILogger<InMemoryRepository<EntityStub>> logger) : base(logger)
    {
    }

    protected override string EntityName => "EntityStub";
  }
  public class EntityStub : Entity
  {
    public string SomeField { get; set; }
  }
}