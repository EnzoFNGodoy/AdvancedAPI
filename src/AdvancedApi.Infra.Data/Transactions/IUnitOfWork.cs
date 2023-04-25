namespace AdvancedApi.Infra.Data.Transactions;

public interface IUnitOfWork
{
    Task<int> Commit();
}