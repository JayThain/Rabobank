using System;

namespace Rabobank.TechnicalTest.GCOB.Models.Exceptions;

public class DuplicateEntityFoundException : Exception
{
  public DuplicateEntityFoundException(string message)
    : base(message)
  {
  }
}