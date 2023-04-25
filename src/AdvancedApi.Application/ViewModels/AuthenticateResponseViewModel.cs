namespace AdvancedApi.Application.ViewModels;

public sealed record AuthenticateResponseViewModel : UserResponseViewModel
{
    public string Token { get; set; } = string.Empty;
}