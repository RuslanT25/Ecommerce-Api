using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Application.Interfaces.UnitOfWorks;
using Ecommerce.Persistence.Context;
using Ecommerce.Persistence.Repositories;

namespace Ecommerce.Persistence.UnitOfWorks;

public class UnitOfWork(EcommerceDbContext context) : IUnitOfWork
{
    private readonly EcommerceDbContext _context = context;

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

    public int SaveChanges() => _context.SaveChanges();

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_context);

    IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_context);
}

