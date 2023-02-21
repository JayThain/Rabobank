namespace Rabobank.TechnicalTest.GCOB.Services.Dtos;

public class CustomerDto
{
  public int Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public AddressDto Address { get; set; }
}