using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SaudiExpress.API.Auth;
using SaudiExpress.API.Extensions;
using SaudiExpress.API.Helpers;
using SaudiExpress.API.ViewModels;
using SaudiExpress.Business.Helpers.Constants;
using SaudiExpress.Business.IServices;
using SaudiExpress.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaudiExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        public AccountController(IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions,
            IAccountService accountService,
            ILogger<AccountController> logger,
            IMapper mapper)
        {
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _accountService = accountService;
            _logger = logger;
            _mapper = mapper;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userInfo = new Business.Models.User.UserInfoIdentity() { Id = Guid.NewGuid().ToString(), UserName = "Ahmed", Email = "A@SHAKER.com", Role = Roles.Admin };
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(userInfo);
            var jwt = await TokensGenerator.GenerateJwt(claimsIdentity, _jwtFactory, userInfo.UserName, _jwtOptions);
            var refreshToken = TokensGenerator.GenerateRefreshToken();
            return Ok(new { JWT = jwt, RefreshToken = refreshToken });

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var userDTO = await _accountService.GetUserByIdAsync(id);
                if (userDTO.Status != ResponseStatus.Succeeded)
                    return this.FailedResponseResult(userDTO);
                return Ok(_mapper.Map<UserViewModel>(userDTO.Result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Exception has been thrown while getting User With id: {id}");
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Exception has been thrown while getting user With id: {id}");
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
