using Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Rabobank.TechnicalTest.GCOB.Models.Entities;

public sealed class Address : Entity
{
  [Required]
  [StringLength(100)]
  public string Street { get; set; }

  [Required]
  [StringLength(100)]
  public string City { get; set; }

  [Required]
  [StringLength(10)]
  public string Postcode { get; set; }

  [Required]
  public int CountryId { get; set; }
}