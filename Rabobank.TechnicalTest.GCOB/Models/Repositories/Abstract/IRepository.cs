using System.Collections.Generic;
using System.Threading.Tasks;
using Rabobank.TechnicalTest.GCOB.Models.Entities.Abstract;

namespace Rabobank.TechnicalTest.GCOB.Models.Repositories.Abstract
{
  public interface IRepository<T> where T : Entity
  {
    Task<int> GenerateIdentityAsync();
    Task<T> GetAsync(int identity);
    Task<IEnumerable<T>> GetAllAsync();
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
  }
}
