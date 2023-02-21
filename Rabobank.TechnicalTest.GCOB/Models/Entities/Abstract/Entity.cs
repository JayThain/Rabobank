using System.ComponentModel.DataAnnotations;

namespace Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract
{
  public abstract class Entity
  {
    [Key]
    public int Id { get; init; }
  }
}
