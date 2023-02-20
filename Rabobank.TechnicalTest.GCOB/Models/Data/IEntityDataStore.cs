using System.Collections.Concurrent;

namespace Rabobank.TechnicalTest.GCOB.Models.Data;

public interface IEntityDataStore<T>
{
    ConcurrentDictionary<int, T> Data { get; init; }
}