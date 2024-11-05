using System.Linq.Expressions;

namespace MySocialApp.Domain;

public interface IService<TEntity> : IDisposable
    where TEntity : Entity
{
    Task<TEntity> Add(TEntity entity);
    Task<IEnumerable<TEntity>> Add(IEnumerable<TEntity> entities);
    Task Update(TEntity entity);
    Task Update(IEnumerable<TEntity> entities);
    Task Remove(TEntity entity);
    Task Remove(Guid id);
    Task Remove(Expression<Func<TEntity, bool>> expression);
}
