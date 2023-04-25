using AdvancedApi.Domain.Entities;
using AdvancedApi.Domain.Interfaces;
using AdvancedApi.Infra.Data.Context;

namespace AdvancedApi.Infra.Data.Repositories;

public sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AdvancedContext context) : base(context)
    { }
}