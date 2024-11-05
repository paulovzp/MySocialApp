using System.Linq.Expressions;

namespace MySocialApp.Domain;

public interface IRepository<TEntity> : IDisposable
    where TEntity : Entity
{

    Task<TEntity> Get(Guid id);
    Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression);
    Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
    Task<Tuple<IEnumerable<TEntity>, int>> Get(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> sorting,
        bool reverse, int page, int pageSize);
    Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Remove(TEntity entity);
}
