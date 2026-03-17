using HarnishBalanceSheet.BusinessLogic;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HarnishBalanceSheet.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IBalanceSheetBL _balanceSheetBl;
        private readonly IConfiguration _configuration;
        public AuthController(IBalanceSheetBL balanceSheetBl, IConfiguration configuration)
        {
            _balanceSheetBl = balanceSheetBl;
            _configuration = configuration;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _balanceSheetBl.GetUserAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
                return Unauthorized();

            var claims = new[] {
                new Claim("UserId", user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
