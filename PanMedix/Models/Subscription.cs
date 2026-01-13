using PanMedix.Enums;

namespace PanMedix.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public SubscriptionStatus SubscriptionStatus { get; set; } = SubscriptionStatus.Pending;

        public DateTime CreatedAt { get; set; }
        public DateTime RenewsAt { get; set; }

        public ICollection<SubscriptionItem> SubscriptionItems { get; set; } = new List<SubscriptionItem>();
    }
}
