namespace MySocialApp.Application;

public interface IUserSession
{
    string Id { get; }
    string Name { get; }
    string Email { get; }
}