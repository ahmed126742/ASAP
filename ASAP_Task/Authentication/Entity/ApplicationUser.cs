using Microsoft.AspNetCore.Identity;

namespace ASAP_Task.WebAPI.Authentication.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public string? JwtId { get; set; }

        public bool IsUsed { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? AddDate { get; set; }
    }
}
