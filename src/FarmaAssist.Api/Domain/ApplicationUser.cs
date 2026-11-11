using Microsoft.AspNetCore.Identity;

namespace FarmaAssist.Api.Domain;

public class ApplicationUser : IdentityUser
{
    public Guid TenantId { get; set; }
    public string Role { get; set; } = "Empleado";
}
