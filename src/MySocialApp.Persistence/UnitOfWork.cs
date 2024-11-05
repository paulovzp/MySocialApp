using MySocialApp.Domain;

namespace MySocialApp.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly MySocialAppContext _context;

    public UnitOfWork(MySocialAppContext context)
    {
        _context = context;
    }

    public async Task<bool> Commit()
    {
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
