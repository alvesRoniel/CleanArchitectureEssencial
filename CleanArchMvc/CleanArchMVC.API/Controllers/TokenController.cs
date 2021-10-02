using CleanArchMvc.Domain.Account;
using CleanArchMVC.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMVC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;

        public TokenController(IAuthenticate authenticate)
        {
            _authenticate = authenticate ??
                throw new ArgumentNullException(nameof(authenticate));
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login(LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
            {
                //return GenerateToken(userInfo);
                return Ok($"User {userInfo.Email} login successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attemp.");
                return BadRequest(ModelState);
            }
        }

        private ActionResult<UserToken> GenerateToken(LoginModel userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
