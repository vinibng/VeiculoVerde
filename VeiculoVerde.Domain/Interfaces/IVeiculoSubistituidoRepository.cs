using VeiculoVerde.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace VeiculoVerde.Domain.Interfaces
{
    public interface IVeiculoSubstituidoRepository
    {
        Task<VeiculoSubstituido> GetByIdAsync(int id);
        Task<IEnumerable<VeiculoSubstituido>> GetAllAsync();
        Task AddAsync(VeiculoSubstituido entity);
        Task UpdateAsync(VeiculoSubstituido entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<VeiculoSubstituido>> FindAsync(Expression<Func<VeiculoSubstituido, bool>> predicate);
        Task<(IEnumerable<VeiculoSubstituido> Items, int TotalCount)> GetPaginatedAsync(int pageNumber, int pageSize, Expression<Func<VeiculoSubstituido, bool>>? predicate = null);
    }
}