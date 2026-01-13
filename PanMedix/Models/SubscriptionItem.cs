namespace PanMedix.Models
{
    public class SubscriptionItem
    {
        public int Id { get; set; }

        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        public int Quantity { get; set; }

        public string? PrescriptionPath { get; set; }
        public bool PrescriptionApproved { get; set; }
    }
}
