namespace Rabobank.TechnicalTest.GCOB.Services.Dtos;

public sealed class AddressDto
{
  public int Id { get; set; }
  public string Street { get; set; }
  public string City { get; set; }
  public string Postcode { get; set; }
  public CountryDto Country { get; set; }
}