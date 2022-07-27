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
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration) 
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = new User { UserName = userForRegistration.UserName, Email = userForRegistration.UserName,RefreshToken="" };

            var result = await _userService.CreateAsync(user, userForRegistration.Password); 
            if (!result) //if (!result.Succeeded) 
            {
                var errors ="Name Already Exist";//result.Errors.Select(e => e.Description);

                return BadRequest(errors); //new RegistrationResponseDto { Errors = errors }
            }
            await _userService.AddToRoleAsync(user, "Viewer");
            return StatusCode(201); 
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userService.FindByNameAsync(userForAuthentication.UserName);

            if (user == null || !await _userService.CheckPasswordAsync(user, userForAuthentication.Password))
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
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value); 
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
                issuer: _jwtSettings.GetSection("validIssuer").Value, 
                audience: _jwtSettings.GetSection("validAudience").Value, 
                claims: claims, 
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)), 
                signingCredentials: signingCredentials); 
            
            return tokenOptions; 
        }
    }
}
