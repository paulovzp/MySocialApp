namespace MySocialApp.Domain;

public class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
    public DateTimeOffset CreateAt { get; private set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdateAt { get; private set; } = DateTimeOffset.Now;

}
