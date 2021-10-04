using Microsoft.EntityFrameworkCore;
using SaudiExpress.Database.EntityModels;
using SaudiExpress.Database.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Database.Repository
{
    internal class ProductRepository : Repository<ProductEntity>, IProductRepository
    {
        private readonly SaudiExpressDatabaseContext _dbcontext;

        public ProductRepository(SaudiExpressDatabaseContext context) : base(context)
        {
            _dbcontext = context;
        }

        public Task<List<ProductEntity>> GetAllProuctsExample()
        {
            return _dbcontext.Products.ToListAsync();
        }
    }
}
