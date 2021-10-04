using AutoMapper;
using SaudiExpress.Business.IServices;
using SaudiExpress.Business.Models;
using SaudiExpress.Business.Models.User;
using SaudiExpress.Database.UnitOfWorkRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> GetUserNameAsync(string userId)
        {
            return (await _unitOfWork.AccountRepo.GetUserByIdAsync(userId)).UserName;
        }
        public async Task<ResponseModel<UserDTO>> GetUserByIdAsync(string userId)
        {
            var userEntity = await _unitOfWork.AccountRepo.GetUserByIdAsync(userId);
            if (userEntity == null)
                return new ResponseModel<UserDTO>(null, ResponseStatus.BadRequest) { Message = "User doesn't exist" };
            else return new ResponseModel<UserDTO>(_mapper.Map<UserDTO>(userEntity));
        }
    }
}
