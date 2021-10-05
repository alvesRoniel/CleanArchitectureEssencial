using CleanArchMvc.Domain.Account;
using CleanArchMVC.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMVC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate ??
                throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login(LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
            {
               return GenerateToken(userInfo);
                //return Ok($"User {userInfo.Email} login successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attemp.");
                return BadRequest(ModelState);
            }
        }
        
        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        public async Task<ActionResult> CreateUser(LoginModel userInfo)
        {
            var result = await _authenticate.RegisterUser(userInfo.Email, userInfo.Password);

            if (result)
            {
                return Ok($"User {userInfo.Email} was create successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attemp.");
                return BadRequest(ModelState);
            }
        }


        private UserToken GenerateToken(LoginModel userInfo)
        {
            //declaracao do usuario
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuValor", "o que vc quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar chave privada para assinar o token
            var privatekey = new SymmetricSecurityKey(
                                 Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //Gerar a assinatura digtial do token
            var credentials = new SigningCredentials(privatekey, SecurityAlgorithms.HmacSha256);

            //Defini o tempo de expiraçao do token
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //Gerar o Token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
