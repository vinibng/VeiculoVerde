
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
    public class ImpactoAmbientalRepository : IImpactoAmbientalRepository
    {
        private readonly ApplicationDbContext _context;

        public ImpactoAmbientalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ImpactoAmbiental entity)
        {
            await _context.ImpactosAmbientais.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ImpactosAmbientais.FindAsync(id);
            if (entity != null)
            {
                _context.ImpactosAmbientais.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ImpactoAmbiental>> FindAsync(Expression<Func<ImpactoAmbiental, bool>> predicate)
        {
            return await _context.ImpactosAmbientais.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<ImpactoAmbiental>> GetAllAsync()
        {
            return await _context.ImpactosAmbientais.ToListAsync();
        }

        public async Task<ImpactoAmbiental> GetByIdAsync(int id)
        {
            return await _context.ImpactosAmbientais.FindAsync(id);
        }

        public async Task UpdateAsync(ImpactoAmbiental entity)
        {
            _context.ImpactosAmbientais.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
