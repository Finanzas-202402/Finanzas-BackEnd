namespace Finanzas_BackEnd.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity>
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> FindByIdAsync(int id);
    void Remove(TEntity entity);
    Task<IEnumerable<TEntity>> ListAsync();
}