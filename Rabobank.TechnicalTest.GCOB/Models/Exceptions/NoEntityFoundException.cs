using System;

namespace Rabobank.TechnicalTest.GCOB.Models.Exceptions;

public class NoEntityFoundException : Exception
{
  public NoEntityFoundException(string message)
    : base(message)
  {
  }
}