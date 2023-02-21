using System;

namespace Rabobank.TechnicalTest.GCOB.Services.Exceptions
{
  public class CustomerNotFoundException : Exception
  {
    public CustomerNotFoundException(string message)
      : base(message)
    {
    }
  }
}
