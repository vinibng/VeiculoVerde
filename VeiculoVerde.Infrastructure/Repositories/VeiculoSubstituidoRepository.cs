
using Microsoft.EntityFrameworkCore;
using VeiculoVerde.Domain.Entities;
using VeiculoVerde.Domain.Interfaces;
using VeiculoVerde.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VeiculoVerde.Infrastructure.Repositories
{
    public class VeiculoSubstituidoRepository : IVeiculoSubstituidoRepository
    {
        private readonly ApplicationDbContext _context;

        public VeiculoSubstituidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(VeiculoSubstituido entity)
        {
            await _context.VeiculosSubstituidos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.VeiculosSubstituidos.FindAsync(id);
            if (entity != null)
            {
                _context.VeiculosSubstituidos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<VeiculoSubstituido>> FindAsync(Expression<Func<VeiculoSubstituido, bool>> predicate)
        {
            return await _context.VeiculosSubstituidos.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<VeiculoSubstituido>> GetAllAsync()
        {
            return await _context.VeiculosSubstituidos.ToListAsync();
        }

        public async Task<VeiculoSubstituido> GetByIdAsync(int id)
        {
            return await _context.VeiculosSubstituidos.FindAsync(id);
        }

        public async Task UpdateAsync(VeiculoSubstituido entity)
        {
            _context.VeiculosSubstituidos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<VeiculoSubstituido> Items, int TotalCount)> GetPaginatedAsync(int pageNumber, int pageSize, Expression<Func<VeiculoSubstituido, bool>>? predicate = null)
        {
            var query = _context.VeiculosSubstituidos.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return (items, totalCount);
        }
    }
}