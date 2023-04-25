using AdvancedApi.Application.Auth;
using AdvancedApi.Application.Interfaces;
using AdvancedApi.Application.ViewModels;
using AdvancedApi.Domain.Entities;
using AdvancedApi.Domain.Enums;
using AdvancedApi.Domain.Interfaces;
using AdvancedApi.Infra.Data.Transactions;
using AutoMapper;

namespace AdvancedApi.Application.Services;

public sealed class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserServices(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region Queries
    public async Task<IEnumerable<UserResponseViewModel>> GetAll()
        => _mapper.Map<IEnumerable<UserResponseViewModel>>
        (await _userRepository.GetAll());

    public async Task<UserResponseViewModel> GetById(Guid id)
         => _mapper.Map<UserResponseViewModel>
        (await _userRepository.GetOneWhere(x => x.Id == id));

    public async Task<AuthenticateResponseViewModel> Login(AuthenticateRequestViewModel viewModel)
    {
        var userExists = await _userRepository.GetOneWhere(x =>
        x.Email == viewModel.Email
        && x.Password == viewModel.Password
        && x.Status == EStatus.Active)
            ?? throw new InvalidOperationException("User specified doesn't exists.");

        var response = _mapper.Map<AuthenticateResponseViewModel>(userExists);
        response.Token = TokenService.GenerateToken(userExists);

        return response;
    }
    #endregion

    #region Commands
    public async Task Create(UserRequestViewModel viewModel)
    {
        var userExists = await _userRepository.GetOneWhere(x =>
        x.Email == viewModel.Email
        && x.Status == EStatus.Active);

        if (userExists is not null)
            throw new InvalidOperationException("An user with this email already exists.");

        var user = _mapper.Map<User>(viewModel);

        await _userRepository.Add(user);

        if (await _unitOfWork.Commit() <= 0)
            throw new InvalidDataException("An error ocurred while saving the User.");
    }

    public async Task Update(Guid id, UserRequestViewModel viewModel)
    {
        var user = await _userRepository.GetOneWhere(x =>
        x.Id == id && x.Status == EStatus.Active)
            ?? throw new InvalidOperationException("User specified doesn't exists.");

        var userExists = await _userRepository.GetOneWhere(x => x.Email == viewModel.Email
        && x.Status == EStatus.Active);

        if(userExists is not null)
            throw new InvalidOperationException("An user with this email already exists.");

        var userMapped = _mapper.Map<User>(viewModel);

        userMapped = _mapper.Map(user, userMapped);

        _userRepository.Update(userMapped);

        if (await _unitOfWork.Commit() <= 0)
            throw new InvalidDataException("An error ocurred while updating the User.");
    }

    public async Task Delete(Guid id)
    {
        var user = await _userRepository.GetOneWhere(x =>
        x.Id == id && x.Status == EStatus.Active)
            ?? throw new InvalidOperationException("User specified doesn't exists.");

        _userRepository.Delete(user);

        if (await _unitOfWork.Commit() <= 0)
            throw new InvalidDataException("An error ocurred while deleting the User.");
    }

    public async Task Activate(Guid id)
    {
        var user = await _userRepository.GetOneWhere(x =>
       x.Id == id && x.Status == EStatus.Active)
           ?? throw new InvalidOperationException("User specified doesn't exists.");

        user.Status = EStatus.Active;

        _userRepository.Update(user);

        if (await _unitOfWork.Commit() <= 0)
            throw new InvalidDataException("An error ocurred while deleting the User.");
    }

    public async Task Deactivate(Guid id)
    {
        var user = await _userRepository.GetOneWhere(x =>
      x.Id == id && x.Status == EStatus.Active)
          ?? throw new InvalidOperationException("User specified doesn't exists.");

        user.Status = EStatus.Inactive;

        _userRepository.Update(user);

        if (await _unitOfWork.Commit() <= 0)
            throw new InvalidDataException("An error ocurred while deleting the User.");
    }
    #endregion
}