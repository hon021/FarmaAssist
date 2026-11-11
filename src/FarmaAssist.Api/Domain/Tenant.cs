namespace FarmaAssist.Api.Domain;

public class Tenant
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

// Entidades que pertenecen a una farmacia; TenantId se asigna al guardar.
public interface ITenantOwned
{
    Guid TenantId { get; set; }
}
