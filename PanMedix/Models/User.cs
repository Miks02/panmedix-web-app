using Microsoft.AspNetCore.Identity;

namespace PanMedix.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? GuardianId { get; set; }
        public User? Guardian { get; set; }

        public bool IsGuardianApproved { get; set; }

        public ICollection<User> Patients { get; set; } = new List<User>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
