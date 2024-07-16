using Ecommerce.Domain.Common;

namespace Ecommerce.Application.Interfaces.Repositories;

public interface IWriteRepository<T> where T : class, IEntityBase, new()
{
    Task AddAsync(T entity);
    Task AddRange(IList<T> entities);
    Task<T> UpdateAsync(T entity);
    Task HardDeleteAsync(T entity);
    Task HardDeleteRangeAsync(IList<T> entities);
}
