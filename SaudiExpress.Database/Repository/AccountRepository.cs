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
    internal class AccountRepository : IAccountRepository
    {
        private readonly ApplicationUserManager _userManager;
        private readonly SaudiExpressDatabaseContext _context;

        public AccountRepository(ApplicationUserManager userManager, SaudiExpressDatabaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public Task<AppUser> GetUserByIdAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }
        public Task<AppUser> GetUserByNameAsync(string userName)
        {
            return _userManager.Users
                                  .Include(user => user.UserRoles)
                                  .ThenInclude(userRole => userRole.Role)
                                  .SingleOrDefaultAsync(user => user.UserName == userName);
        }
    }
}
