

using Microsoft.AspNetCore.Identity;

namespace OurEstheticSolution.Models

{
    public class User : IdentityUser

    {
        public string?  FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        public string? Gender { get; set; }

        public string? Address { get; set; }
    }

    
}
