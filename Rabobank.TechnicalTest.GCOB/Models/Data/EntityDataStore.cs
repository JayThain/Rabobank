using System.Collections.Concurrent;

namespace Rabobank.TechnicalTest.GCOB.Models.Data;

public sealed class EntityDataStore<T> : IEntityDataStore<T>
{
    private static EntityDataStore<T> _instance;
    private static readonly object DataLock = new();

    private EntityDataStore()
    {
        Data = new ConcurrentDictionary<int, T>();
    }

    public static IEntityDataStore<T> Instance
    {
        get
        {
            lock (DataLock)
            {
                return _instance ??= new EntityDataStore<T>();
            }
        }
    }

    public ConcurrentDictionary<int, T> Data { get; init; }
}