using FarmaAssist.Api.Domain;
using FarmaAssist.Api.Tenancy;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FarmaAssist.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options, ITenantContext tenant)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Consultation> Consultations => Set<Consultation>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tenant>(e =>
        {
            e.HasIndex(t => t.Slug).IsUnique();
        });

        builder.Entity<ApplicationUser>(e =>
        {
            e.HasIndex(u => u.TenantId);
        });

        builder.Entity<Patient>(e =>
        {
            e.HasIndex(p => new { p.TenantId, p.DocumentType, p.DocumentNumber }).IsUnique();
            e.HasQueryFilter(p => p.TenantId == tenant.TenantId);
        });

        builder.Entity<Consultation>(e =>
        {
            e.HasIndex(c => new { c.TenantId, c.PatientId });
            e.HasOne(c => c.Patient)
             .WithMany(p => p.Consultations)
             .HasForeignKey(c => c.PatientId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasQueryFilter(c => c.TenantId == tenant.TenantId);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (EntityEntry entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            if (entry.Entity is not ITenantOwned owned)
            {
                continue;
            }

            if (tenant.TenantId == Guid.Empty)
            {
                throw new InvalidOperationException("No se pudo resolver la farmacia del usuario.");
            }

            owned.TenantId = tenant.TenantId;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
