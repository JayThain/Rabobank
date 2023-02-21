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
}
