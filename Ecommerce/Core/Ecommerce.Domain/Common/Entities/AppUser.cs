using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Common.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
