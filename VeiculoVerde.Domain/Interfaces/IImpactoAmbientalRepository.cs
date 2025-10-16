
using VeiculoVerde.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace VeiculoVerde.Domain.Interfaces
{
    public interface IImpactoAmbientalRepository
    {
        Task<ImpactoAmbiental> GetByIdAsync(int id);
        Task<IEnumerable<ImpactoAmbiental>> GetAllAsync();
        Task AddAsync(ImpactoAmbiental entity);
        Task UpdateAsync(ImpactoAmbiental entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<ImpactoAmbiental>> FindAsync(Expression<Func<ImpactoAmbiental, bool>> predicate);
    }
}
