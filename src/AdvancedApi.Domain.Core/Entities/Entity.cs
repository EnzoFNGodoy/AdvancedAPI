using AdvancedApi.Domain.Core.Enums;
using Flunt.Notifications;

namespace AdvancedApi.Domain.Core.Entity;

public abstract class Entity : Notifiable<Notification>
{
    protected Entity()
    { }

    protected Entity(Guid id)
    { 
        Id = id;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Status = EStatus.Active;
    }

    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public EStatus Status { get; protected set; }

    public void Update()
        => UpdatedAt = DateTime.UtcNow;

    public void Activate()
        => Status = EStatus.Active;

    public void Deactivate()
        => Status = EStatus.Inactive;

    public abstract void Validate();
}
