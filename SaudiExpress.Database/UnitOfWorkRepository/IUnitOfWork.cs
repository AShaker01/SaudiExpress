using SaudiExpress.Database.IRepository;
using System.Threading.Tasks;

namespace SaudiExpress.Database.UnitOfWorkRepository
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepo { get; }
        IProductRepository ProductRepo { get; }

        int Complete();

        Task<int> CompleteAsync();
    }
}