using System.ComponentModel.DataAnnotations;

namespace PanMedix.Enums
{
    public enum SubscriptionStatus
    {
        [Display(Name = "Odobreno")]
        Approved,

        [Display(Name = "Na čekanju")]
        Pending,

        [Display(Name = "Suspendovano")]
        Suspended,

        [Display(Name = "Otkazano")]
        Cancelled
    }
}
