using SaudiExpress.Business.Models;
using SaudiExpress.Business.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Business.IServices
{
    public interface IAccountService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<ResponseModel<UserDTO>> GetUserByIdAsync(string userId);
    }
}
