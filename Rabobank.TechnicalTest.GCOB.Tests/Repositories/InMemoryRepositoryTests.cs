using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabobank.TechnicalTest.GCOB.Models.Data;
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
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith0Items());
    Assert.AreEqual(1, repository.GenerateIdentityAsync().Result);
  }

  [TestMethod]
  public void GivenIHave1ItemsOfData_ThenNextID_ShouldBe2()
  {
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith1Item());
    Assert.AreEqual(2, repository.GenerateIdentityAsync().Result);
  }

  [TestMethod]
  public void GivenIHave5ItemsOfData_ThenNextID_ShouldBe6()
  {
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith5Items());
    Assert.AreEqual(6, repository.GenerateIdentityAsync().Result);
  }

  // Testing GetAsync
  [TestMethod]
  public void GivenIHave0ItemsOfData_ThenIShouldSeeANotFoundException()
  {
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith0Items());
    Assert.ThrowsExceptionAsync<NoEntityFoundException>(() => repository.GetAsync(1));
  }

  [TestMethod]
  public void GivenIHave1ItemOfData_ThenIShouldFindThatItem()
  {
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith1Item());
    var entity = repository.GetAsync(1);
    Assert.AreEqual("x", entity.Result.SomeField);
  }

  [TestMethod]
  public void GivenIHave5ItemOfData_ThenIShouldFindASpecificItem()
  {
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith5Items());
    var entity = repository.GetAsync(3);
    Assert.AreEqual("c", entity.Result.SomeField);
  }

  // Testing InsertAsync
  [TestMethod]
  public void GivenIHave0ItemOfData_ThenIShouldBeAbleToInsertAnItem()
  {
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith0Items());
    var entity = new EntityStub { Id = repository.GenerateIdentityAsync().Result, SomeField = "x" };
    repository.InsertAsync(entity);

    Assert.AreEqual(2, repository.GenerateIdentityAsync().Result);
    Assert.AreEqual("x", repository.GetAsync(entity.Id).Result.SomeField);
  }

  [TestMethod]
  public void GivenIHave1ItemOfData_ThenIShouldNotBeAbleToInsertAnItemWithSameId()
  {
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith1Item());
    var entity = new EntityStub { Id = 1, SomeField = "x" };
    Assert.ThrowsExceptionAsync<DuplicateEntityFoundException>(() => repository.InsertAsync(entity));
  }

  // Testing UpdateAsync
  [TestMethod]
  public void GivenIHave1ItemOfData_ThenIShouldBeAbleToUpdateThatItem()
  {
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith1Item());
    
    var entity = repository.GetAsync(1).Result;

    entity.SomeField = "new value";
    repository.UpdateAsync(entity);

    Assert.AreEqual(entity.SomeField, repository.GetAsync(1).Result.SomeField);
  }

  [TestMethod]
  public void GivenIHave1ItemOfData_ThenIShouldNotBeAbleToUpdateTheItemIfItDoesNotExist()
  {
    var repository = new InMemoryRepositoryStub(_logger, DataStoreWith1Item());

    var entity = new EntityStub { Id = 10, SomeField = "Anything" };

    Assert.ThrowsExceptionAsync<NoEntityFoundException>(() => repository.UpdateAsync(entity));
  }

  // Helper methods and classes
  private static IEntityDataStore<EntityStub> DataStoreWith0Items()
  {
    ClearDataStore();
    return EntityDataStore<EntityStub>.Instance;
  }

  private static IEntityDataStore<EntityStub> DataStoreWith1Item()
  {
    ClearDataStore();
    var dataStore = EntityDataStore<EntityStub>.Instance;
    dataStore.Data.TryAdd(1, new EntityStub { Id = 1, SomeField = "x" });
    return dataStore;
  }

  private static IEntityDataStore<EntityStub> DataStoreWith5Items()
  {
    ClearDataStore();
    var dataStore = EntityDataStore<EntityStub>.Instance;

    dataStore.Data.TryAdd(1, new EntityStub { Id = 1, SomeField = "a" });
    dataStore.Data.TryAdd(2, new EntityStub { Id = 2, SomeField = "b" });
    dataStore.Data.TryAdd(3, new EntityStub { Id = 3, SomeField = "c" });
    dataStore.Data.TryAdd(4, new EntityStub { Id = 4, SomeField = "d" });
    dataStore.Data.TryAdd(5, new EntityStub { Id = 5, SomeField = "e" });

    return dataStore;
  }

  private static void ClearDataStore()
  {
    var dataStore = EntityDataStore<EntityStub>.Instance;
    dataStore.Data.Clear();
  }

  public class InMemoryRepositoryStub : InMemoryRepository<EntityStub>
  {
    public InMemoryRepositoryStub(ILogger<InMemoryRepository<EntityStub>> logger, IEntityDataStore<EntityStub> entityDataStore) : base(logger, entityDataStore)
    {
    }
    protected override string EntityName => "EntityStub";
  }

    public class EntityStub : Entity
  {
    public string SomeField { get; set; }
  }
}