using Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Entities;

public sealed class Address : Entity
{
  public string Street { get; set; }
  public string City { get; set; }
  public string Postcode { get; set; }
  public int CountryId { get; set; }
}