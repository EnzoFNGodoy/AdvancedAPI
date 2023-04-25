using AdvancedApi.Application.ViewModels;
using AdvancedApi.Domain.Entities;
using AutoMapper;

namespace AdvancedApi.Application.AutoMapper;

public sealed class ViewModelToDomainProfile : Profile
{
    public ViewModelToDomainProfile()
    {
        CreateMap<UserRequestViewModel, User>();
    }
}