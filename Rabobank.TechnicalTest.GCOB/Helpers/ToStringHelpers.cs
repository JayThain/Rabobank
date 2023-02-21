using System.Text.Json;

namespace Rabobank.TechnicalTest.GCOB.Helpers;

public static class ToStringHelpers
{
  public static string ToJson<T>(this T entity)
  {
    return entity == null ? "null" : JsonSerializer.Serialize(entity);
  }
}
