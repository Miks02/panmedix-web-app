using PanMedix.Enums;

namespace PanMedix.Models
{
    public class Medicine
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImagePath { get; set; } = null!;

        public MedicineType MedicineType { get; set; }

        public decimal Price { get; set; }

        public bool RequiresReceipt { get; set; }
    }
}
