using System.Linq.Expressions;

namespace MySocialApp.Domain;

internal class Service<TEntity> : IService<TEntity>
    where TEntity : Entity
{
    protected readonly IRepository<TEntity> _repository;

    public Service(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        await _repository.Add(entity);
        return entity;
    }

    public async virtual Task<IEnumerable<TEntity>> Add(IEnumerable<TEntity> entities)
    {
        HashSet<TEntity> entitesCreated = new();

        foreach (var entity in entities)
        {
            await _repository.Add(entity);
            entitesCreated.Add(entity);
        }

        return entitesCreated;
    }

    public void Dispose()
    {
        _repository.Dispose();
    }

    public async Task Remove(TEntity entity)
    {
        await _repository.Remove(entity);
    }

    public async Task Remove(Guid id)
    {
        var entity = await _repository.Get(id);
        await _repository.Remove(entity);

    }

    public async Task Remove(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _repository.GetSingle(expression);
        await _repository.Remove(entity);
    }

    public async Task Update(TEntity entity)
    {
        await _repository.Update(entity);
    }

    public async Task Update(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            await Update(entity);
        }
    }
}