using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SaudiExpress.API.Auth;
using SaudiExpress.API.Extensions;
using SaudiExpress.API.Helpers;
using SaudiExpress.Business.Helpers.Constants;
using SaudiExpress.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaudiExpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        public AuthController(IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userInfo = new Business.Models.User.UserInfoIdentity() { Id = Guid.NewGuid().ToString(), UserName = "Ahmed", Email = "A@SHAKER.com", Role = Roles.Admin };
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(userInfo);
            var jwt = await TokensGenerator.GenerateJwt(claimsIdentity, _jwtFactory, userInfo.UserName, _jwtOptions);
            var refreshToken = TokensGenerator.GenerateRefreshToken();
            return Ok(new { JWT = jwt, RefreshToken = refreshToken});

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ResponseModel<string> model = new("Value");
            if (model.Status != ResponseStatus.Succeeded)
                return this.FailedResponseResult(model);
            return this.Created();
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
