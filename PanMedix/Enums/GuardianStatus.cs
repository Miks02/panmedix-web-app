using System.ComponentModel.DataAnnotations;

namespace PanMedix.Enums;

public enum GuardianStatus
{
    NotGuardian,
    [Display(Name = "Odobren")]
    Approved,
    [Display(Name = "Na čekanju")]
    Pending,
    [Display(Name = "Odbijen")]
    Denied,
}