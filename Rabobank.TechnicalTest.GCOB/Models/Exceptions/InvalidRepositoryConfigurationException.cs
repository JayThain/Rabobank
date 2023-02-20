using System;

namespace Rabobank.TechnicalTest.GCOB.Models.Exceptions
{
  public class InvalidRepositoryConfigurationException: Exception
  {
    public InvalidRepositoryConfigurationException(string message)
      : base(message)
    {
    }
  }

  public class NoEntityFoundException : Exception
  {
    public NoEntityFoundException(string message)
      : base(message)
    {
    }
  }

  public class DuplicateEntityFoundException : Exception
  {
    public DuplicateEntityFoundException(string message)
      : base(message)
    {
    }
  }
}
