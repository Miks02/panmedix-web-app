using System.ComponentModel.DataAnnotations;

namespace PanMedix.Enums
{
    public enum MedicineType
    {
        [Display(Name = "Antibiotici")]
        Antibiotics,

        [Display(Name = "Analgetici")]
        Analgetics,

        [Display(Name = "Vitamini")]
        Vitamins,

        [Display(Name = "Antipiretici")]
        Antipyretics,

        [Display(Name = "Probiotici")]
        Probiotics,

        [Display(Name = "Antidepresivi")]
        Antidepressants,

        [Display(Name = "Kardiovaskularni")]
        Cardiovascular,

        [Display(Name = "Dijabetici")]
        Diabetics,

        [Display(Name = "Antialergeni")]
        Antiallergenics,

        [Display(Name = "Antivirusni")]
        Antivirals,

        [Display(Name = "Ostali")]
        Other
    }
}
