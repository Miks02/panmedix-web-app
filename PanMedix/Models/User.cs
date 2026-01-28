using Microsoft.AspNetCore.Identity;
using PanMedix.Enums;

namespace PanMedix.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        
        public string? PharmacyName { get; set; }

        public string ImagePath { get; set; } = null!;

        public GuardianStatus GuardianStatus { get; set; } = GuardianStatus.NotGuardian;

        public string? GuardianId { get; set; }
        public User? Guardian { get; set; }

        public bool IsGuardianApproved { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<User> Patients { get; set; } = new List<User>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
