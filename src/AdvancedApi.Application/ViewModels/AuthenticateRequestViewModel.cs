namespace AdvancedApi.Application.ViewModels;

public sealed record AuthenticateRequestViewModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}