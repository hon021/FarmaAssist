using FarmaAssist.Api.Data;
using FarmaAssist.Api.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FarmaAssist.Api.Endpoints;

public static class PatientEndpoints
{
    public static RouteGroupBuilder MapPatientEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/patients").RequireAuthorization();

        group.MapGet("/", async Task<Ok<List<PatientDto>>> (AppDbContext db, CancellationToken ct) =>
        {
            var patients = await db.Patients
                .AsNoTracking()
                .OrderBy(p => p.FullName)
                .Take(200)
                .Select(p => new PatientDto(p.Id, p.DocumentType, p.DocumentNumber, p.FullName, p.BirthDate, p.Phone))
                .ToListAsync(ct);

            return TypedResults.Ok(patients);
        });

        group.MapPost("/", async Task<Results<Created<PatientDto>, Conflict<string>>> (
            CreatePatientRequest request, AppDbContext db, CancellationToken ct) =>
        {
            var exists = await db.Patients.AnyAsync(
                p => p.DocumentType == request.DocumentType && p.DocumentNumber == request.DocumentNumber, ct);

            if (exists)
            {
                return TypedResults.Conflict("Ya existe un paciente con ese documento en esta farmacia.");
            }

            var patient = new Patient
            {
                DocumentType = request.DocumentType,
                DocumentNumber = request.DocumentNumber,
                FullName = request.FullName,
                BirthDate = request.BirthDate,
                Phone = request.Phone
            };

            db.Patients.Add(patient);
            await db.SaveChangesAsync(ct);

            var dto = new PatientDto(patient.Id, patient.DocumentType, patient.DocumentNumber,
                patient.FullName, patient.BirthDate, patient.Phone);

            return TypedResults.Created($"/api/patients/{patient.Id}", dto);
        });

        return group;
    }
}

public record CreatePatientRequest(string DocumentType, string DocumentNumber, string FullName, DateOnly? BirthDate, string? Phone);

public record PatientDto(Guid Id, string DocumentType, string DocumentNumber, string FullName, DateOnly? BirthDate, string? Phone);
