using PanMedix.ViewModels;

namespace PanMedix.Services.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterViewModel request);
    Task LoginAsync(LoginViewModel request);
    Task LogoutAsync();
}