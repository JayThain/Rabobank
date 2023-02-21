using Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Rabobank.TechnicalTest.GCOB.Models.Entities;

public sealed class Country : Entity
{
  [Required]
  [StringLength(100)]
  public string Name { get; set; }
}