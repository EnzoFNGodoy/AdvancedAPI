using AdvancedApi.Application.ViewModels;
using AdvancedApi.Domain.Entities;
using AutoMapper;

namespace AdvancedApi.Application.AutoMapper;

public sealed class DomainToViewModelProfile : Profile
{
    public DomainToViewModelProfile()
    {
        CreateMap<User, UserResponseViewModel>();
        CreateMap<User, AuthenticateResponseViewModel>();
    }
}