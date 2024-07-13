using Ecommerce.Application.Interfaces.Repositories;
using Ecommerce.Domain.Common;

namespace Ecommerce.Application.Interfaces.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase, new();
    IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new();
    Task<int> SaveChangesAsync();
    int SaveChanges();
}

