using AdvancedApi.Application.ViewModels;

namespace AdvancedApi.Application.Interfaces;

public interface IUserServices
{
    Task<IEnumerable<UserResponseViewModel>> GetAll();
    Task<UserResponseViewModel> GetById(Guid id);
    Task<AuthenticateResponseViewModel> Login(AuthenticateRequestViewModel viewModel);

    Task Create(UserRequestViewModel viewModel);
    Task Update(Guid id, UserRequestViewModel viewModel);
    Task Delete(Guid id);
    Task Activate(Guid id);
    Task Deactivate(Guid id);
}