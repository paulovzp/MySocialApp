using Microsoft.EntityFrameworkCore;
using MySocialApp.Domain;
using System.Linq.Expressions;

namespace MySocialApp.Persistence;

internal class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity

{
    protected readonly MySocialAppContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(MySocialAppContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public Task Add(TEntity entity)
    {
        _dbSet.AddAsync(entity);
        return Task.CompletedTask;
    }

    public Task Remove(TEntity entity)
    {
        return Task.FromResult(_dbSet.Remove(entity));
    }

    public Task Update(TEntity entity)
    {
        return Task.FromResult(_dbSet.Update(entity));
    }

    public async Task<TEntity> Get(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.Where(expression).ToListAsync();
    }

    public Task<bool> Exists(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.Where(expression).AnyAsync();
    }

    public async Task<Tuple<IEnumerable<TEntity>, int>> Get(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> sorting, bool reverse, int page, int pageSize)
    {
        var query = _dbSet.Where(expression);

        if (reverse)
            query = query.OrderByDescending(sorting);
        else
            query = query.OrderBy(sorting);

        int totalCount = await query.CountAsync();
        var result = await query.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
        return new Tuple<IEnumerable<TEntity>, int>(result.AsEnumerable(), totalCount);
    }

    public void Dispose() => _dbContext.Dispose();

    public Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.FirstOrDefaultAsync(expression);
    }
}
