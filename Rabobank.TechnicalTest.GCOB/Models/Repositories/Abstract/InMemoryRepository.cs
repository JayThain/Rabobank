using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract;
using Rabobank.TechnicalTest.GCOB.Models.Exceptions;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract;

public abstract class InMemoryRepository<T> where T : Entity
{
  protected readonly ILogger<InMemoryRepository<T>> Logger;

  protected ConcurrentDictionary<int, T> Data { get; init; }

  private readonly object _identityLock = new();

  protected InMemoryRepository(ILogger<InMemoryRepository<T>> logger)
  {
    Logger = logger;
  }

  protected abstract string EntityName { get; }

  public Task<int> GenerateIdentityAsync()
  {
    Logger.LogDebug($"Generating {EntityName} identity");

    return Task.Run(() =>
    {
      lock (_identityLock)
      {
        CheckDataInstantiated();

        if (Data.Count == 0) return 1;

        var x = Data.Values.Max(c => c.Id);
        return ++x;
      }
    });
  }
  
  public Task<T> GetAsync(int identity)
  {
    Logger.LogDebug($"Find {EntityName} with identity {identity}");

    CheckDataInstantiated();

    if (!Data.ContainsKey(identity)) throw new NoEntityFoundException($"No {EntityName} exists with identity {identity.ToString()}");
    Logger.LogDebug($"Found {EntityName} with identity {identity}");
    return Task.FromResult(Data[identity]);
  }

  public Task InsertAsync(T entity)
  {
    CheckDataInstantiated();

    if (Data.ContainsKey(entity.Id))
    {
      throw new DuplicateEntityFoundException(
        $"Cannot insert {EntityName} with identity '{entity.Id}' as it already exists in the collection");
    }

    Data.TryAdd(entity.Id, entity);
    Logger.LogDebug($"New {EntityName} inserted [ID:{entity.Id}]. There are now {Data.Count} legal entities in the store.");
    return Task.FromResult(entity);
  }

  public Task UpdateAsync(T entity)
  {
    CheckDataInstantiated();

    if (!Data.ContainsKey(entity.Id))
    {
      throw new NoEntityFoundException(
        $"Cannot update {EntityName} with identity '{entity.Id}' as it doesn't exist");
    }

    Data[entity.Id] = entity;
    Logger.LogDebug($"New {EntityName} updated [ID:{entity.Id}].");

    return Task.FromResult(entity);
  }

  private void CheckDataInstantiated()
  {
    if (Data == null)
      throw new InvalidRepositoryConfigurationException($"Please ensure that {EntityName} has its Data field instantiated");
  }
}