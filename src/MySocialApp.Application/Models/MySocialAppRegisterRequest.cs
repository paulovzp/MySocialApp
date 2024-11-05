namespace MySocialApp.Application;

public sealed class MySocialAppRegisterRequest
{
    /// <summary>
    /// The user's email address which acts as a user name.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The user's password.
    /// </summary>
    public required string UserName { get; init; }

    /// <summary>
    /// The user's password.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The user's password.
    /// </summary>
    public required string Password { get; init; }

}
