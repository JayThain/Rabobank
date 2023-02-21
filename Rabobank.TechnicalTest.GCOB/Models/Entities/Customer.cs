using Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Rabobank.TechnicalTest.GCOB.Models.Entities;

public class Customer : Entity
{
  [Required]
  [StringLength(50)]
  public string FirstName { get; set; }

  [Required]
  [StringLength(50)]
  public string LastName { get; set; }

  [Required]
  public int AddressId { get; set; }
}