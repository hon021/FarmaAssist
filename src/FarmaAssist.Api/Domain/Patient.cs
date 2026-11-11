namespace FarmaAssist.Api.Domain;

public class Patient : ITenantOwned
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    public required string DocumentType { get; set; }
    public required string DocumentNumber { get; set; }
    public required string FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Phone { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public ICollection<Consultation> Consultations { get; set; } = [];
}

public class Consultation : ITenantOwned
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TenantId { get; set; }
    public Guid PatientId { get; set; }
    public Patient? Patient { get; set; }
    public required string Reason { get; set; }
    public string? Result { get; set; }
    public string? CreatedByUserId { get; set; }
    public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;
}
