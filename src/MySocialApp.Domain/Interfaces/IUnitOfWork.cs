namespace MySocialApp.Domain;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Commit();
}
