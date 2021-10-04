using SaudiExpress.Database.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Database.IRepository
{
    public interface IProductRepository : IRepository<ProductEntity>
    {
        public Task<List<ProductEntity>> GetAllProuctsExample();
    }
}
