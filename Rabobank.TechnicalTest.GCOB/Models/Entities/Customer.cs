using Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Entities;

public class Customer : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AddressId { get; set; }
}