using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Domain.Common;
using Ecommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
{
    private readonly EcommerceDbContext _context;

    public WriteRepository(EcommerceDbContext context)
    {
        _context = context;
    }

    private DbSet<T> _dbSet { get => _context.Set<T>(); }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRange(IList<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task HardDeleteAsync(T entity)
    {
        await Task.Run(() => _dbSet.Remove(entity));
    }

    public async Task<T> UpdateAsync(T entity)
    {
        await Task.Run(() => _dbSet.Update(entity));
        return entity;
    }
}

