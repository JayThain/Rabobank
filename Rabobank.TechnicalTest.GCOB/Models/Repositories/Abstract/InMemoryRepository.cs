using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

public abstract class InMemoryRepository<T> where T : Entity
{
  protected readonly ILogger<InMemoryRepository<T>> Logger;

  protected ConcurrentDictionary<int, T> Data { get; init; }

  protected InMemoryRepository(ILogger<InMemoryRepository<T>> logger)
  {
    Logger = logger;
  }

  protected abstract string EntityName { get; }

  protected Task<int> GenerateIdentityAsync()
  {
    Logger.LogDebug("Generating Customer identity");

    return Task.Run(() =>
    {
      if (Data.Count == 0) return 1;

      var x = Data.Values.Max(c => c.Id);
      return ++x;
    });
  }
  
  public Task<T> GetAsync(int identity)
  {
    Logger.LogDebug($"FindMany Customers with identity {identity}");

    if (!Data.ContainsKey(identity)) throw new Exception(identity.ToString());
    Logger.LogDebug($"Found Customer with identity {identity}");
    return Task.FromResult(Data[identity]);

  }

  public Task InsertAsync(T entity)
  {
    if (Data.ContainsKey(entity.Id))
    {
      throw new Exception(
        $"Cannot insert {EntityName} with identity '{entity.Id}' as it already exists in the collection");
    }

    Data.TryAdd(entity.Id, entity);
    Logger.LogDebug($"New {EntityName} inserted [ID:{entity.Id}]. There are now {Data.Count} legal entities in the store.");
    return Task.FromResult(entity);
  }

  public Task UpdateAsync(T entity)
  {
    if (!Data.ContainsKey(entity.Id))
    {
      throw new Exception(
        $"Cannot update {EntityName} with identity '{entity.Id}' as it doesn't exist");
    }

    Data[entity.Id] = entity;
    Logger.LogDebug($"New {EntityName} updated [ID:{entity.Id}].");

    return Task.FromResult(entity);
  }
}