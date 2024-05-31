using GiftCardManagementSystem.APIGateway.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GiftCardManagementSystem.APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly RabbitMqConcurrencyService _concurrencyService;

        private static readonly Dictionary<string, string> RefreshTokens = new Dictionary<string, string>();

        public AuthController(IConfiguration configuration,
            RabbitMqConcurrencyService concurrencyService
            )
        {
            _configuration = configuration;
            _concurrencyService = concurrencyService;
        }

        [HttpPost("GetToken")]
        public IActionResult GetToken([FromBody] LoginModel model)
        {
            _concurrencyService.PublishMessage("refresh_request");
          
            // Validate username and password
            if (!ValidateUser(model.Username, model.Password))
            {
                return Unauthorized();
            }

            var accessToken = GenerateJwtToken(model.Username);
            var refreshToken = GenerateRefreshToken();

            StoreRefreshToken(model.Username, refreshToken);
            SetRefreshTokenCookie(refreshToken);
            var tokenExpiration = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:RefreshTokenValidityInDays"]));

            return Ok(new
            {
                AccessToken = accessToken,
                TokenExpiration = tokenExpiration
            });
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return BadRequest("Refresh token not found");
            }

            // Validate the refresh token
            var username = ValidateRefreshToken(refreshToken);
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Invalid refresh token");
            }

            // Generate a new access token
            var newAccessToken = GenerateJwtToken(username);
            var newRefreshToken = GenerateRefreshToken();

            // Store the new refresh token securely
            StoreRefreshToken(username, newRefreshToken);

            // Update the refresh token cookie
            SetRefreshTokenCookie(newRefreshToken);

            return Ok(new { AccessToken = newAccessToken });
        }

        private bool ValidateUser(string userName, string password)
        {
            const string validUserName = "Admin";
            const string validPassword = "Admin@2024";

            return userName == validUserName && password == validPassword;
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            int.TryParse(_configuration["Jwt:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private void StoreRefreshToken(string username, string refreshToken)
        {
            if (RefreshTokens.ContainsKey(username))
            {
                RefreshTokens[username] = refreshToken;
            }
            else
            {
                RefreshTokens.Add(username, refreshToken);
            }
        }

        private string ValidateRefreshToken(string refreshToken)
        {
            foreach (var kvp in RefreshTokens)
            {
                if (kvp.Value == refreshToken)
                {
                    return kvp.Key;
                }
            }
            return null;
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:RefreshTokenValidityInDays"]));
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshTokenExpiration,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}