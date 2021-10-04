using SaudiExpress.Database.IRepository;
using SaudiExpress.Database.Repository;
using System.Threading.Tasks;

namespace SaudiExpress.Database.UnitOfWorkRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAccountRepository _accountRepo;
        private IProductRepository _productRepo;
        private readonly SaudiExpressDatabaseContext _saudiExpressContext;
        private readonly ApplicationUserManager _userManager;

        public UnitOfWork(SaudiExpressDatabaseContext saudiExpressContext, ApplicationUserManager userManager)
        {
            _saudiExpressContext = saudiExpressContext;
            _userManager = userManager;
        }

        public IAccountRepository AccountRepo
        {
            get
            {
                if (_accountRepo == null)
                {
                    _accountRepo = new AccountRepository(_userManager, _saudiExpressContext);
                }
                return _accountRepo;
            }
        }

        public IProductRepository ProductRepo
        {
            get
            {
                if (_productRepo == null)
                {
                    _productRepo = new ProductRepository(_saudiExpressContext);
                }
                return _productRepo;
            }
        }

        public int Complete()
        {
            return _saudiExpressContext.SaveChanges();
        }

        public Task<int> CompleteAsync()
        {
            return _saudiExpressContext.SaveChangesAsync();
        }
    }
}