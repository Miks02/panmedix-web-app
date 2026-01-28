using PanMedix.Enums;

namespace PanMedix.ViewModels;

public class GuardianViewModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public GuardianStatus Status { get; set; }
    public int NumberOfWards { get; set; }
}