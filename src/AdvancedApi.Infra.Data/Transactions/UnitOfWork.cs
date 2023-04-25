using AdvancedApi.Infra.Data.Context;

namespace AdvancedApi.Infra.Data.Transactions;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AdvancedContext _context;

    public UnitOfWork(AdvancedContext context)
    {
        _context = context;
    }

    public async Task<int> Commit()
        => await _context.SaveChangesAsync();
}