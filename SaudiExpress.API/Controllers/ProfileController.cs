using CompoundPlating.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaudiExpress.Business.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudiExpress.API.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ProfileController : ControllerBase
    {
        private readonly ClaimsPrincipalService _principalService;
        public ProfileController(ClaimsPrincipalService claimsPrincipalService)
        {
            _principalService = claimsPrincipalService;
        }
        [HttpGet()]
        public List<string> Get()
        {
            return new List<string>() { $"Hello Logged User Id - {_principalService.GetCurrentUserId()}" };
        }
    }
}
