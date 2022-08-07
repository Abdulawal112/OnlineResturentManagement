using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OnlineResturnatManagement.Server.Helper;
using OnlineResturnatManagement.Server.Models;
using OnlineResturnatManagement.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineResturnatManagement.Server.Services.IService;
using OnlineResturnatManagement.Server.Services.Service;
using NLog.Targets;

namespace OnlineResturnatManagement.Server.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        //private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AccountsController(IConfiguration configuration,ITokenService tokenService,IUserService userService)
        {
            //_userManager = userManager;
            _configuration = configuration;
           // _jwtSettings = _configuration.GetSection("JwtSettings");
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration) 
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();
            if (userForRegistration.UserName == "" || userForRegistration.Password == "")
                return BadRequest();

            var user = new User { UserName = userForRegistration.UserName, Email = userForRegistration.UserName,RefreshToken="" };

            await _userService.AddToRoleAsync(user, "Viewer");
            User findUser = await _userService.FindByNameAsync(user.UserName);

            if (findUser == null) 
            {
                var result = await _userService.CreateAsync(user, userForRegistration.Password);
                return StatusCode(201);
                
            }
            else
            {
                var errors = "Name Already Exist";
                return BadRequest(errors);
            }
           
           
           
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userService.FindByNameAsync(userForAuthentication.UserName);

            if (user == null || !_userService.CheckPasswordAsync(user, userForAuthentication.Password))
                    return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            
            var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(user);
            var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            user.RefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            
            await _userService.UpdateAsync(user);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, RefreshToken = user.RefreshToken });
        }

        private SigningCredentials GetSigningCredentials() 
        { 
            var key = Encoding.UTF8.GetBytes(JwtSettingHelper.SecurityKey); 
            var secret = new SymmetricSecurityKey(key); 
            
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256); 
        }

        private List<Claim> GetClaims(IdentityUser user) 
        { 
            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Name, user.Email) 
            }; 
            
            return claims; 
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) 
        { 
            var tokenOptions = new JwtSecurityToken(
                issuer: JwtSettingHelper.ValidIssuer, 
                audience: JwtSettingHelper.ValidAudience, 
                claims: claims, 
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(JwtSettingHelper.EexpiryInMinutes)), 
                signingCredentials: signingCredentials); 
            
            return tokenOptions; 
        }


    }
}
