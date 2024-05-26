using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
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

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("GetToken")]
        public IActionResult GetToken([FromBody] LoginModel model)
        {
            // Validate username and password
            if (!ValidateUser(model.Username, model.Password))
            {
                return Unauthorized();
            }

            var accessToken = GenerateJwtToken(model.Username);
            var refreshToken = GenerateRefreshToken();

            // Store the refresh token securely, e.g., in a database associated with the user

            // Calculate expiration date for refresh token
            var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(1); // Example: Refresh token expires in 1 minute
            var tokenExpiration = DateTime.UtcNow.AddMinutes(1); // Example: Refresh token expires in 1 minute

            return Ok(new
            {
                AccessToken = accessToken,
                TokenExirpedDate = tokenExpiration,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = refreshTokenExpiration
            });
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequestModel model)
        {
            // Validate the refresh token
            if (!ValidateRefreshToken(model.RefreshToken))
            {
                return BadRequest("Invalid refresh token");
            }

            // If the refresh token is valid, generate a new access token
            var newAccessToken = GenerateJwtToken(GetUsernameFromRefreshToken(model.RefreshToken));

            // Return the new access token to the client
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

           int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            // Generate a random refresh token
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private bool ValidateRefreshToken(string refreshToken)
        {
            // Your refresh token validation logic here
            // For demonstration purposes, always return true
            return true;
        }

        private string GetUsernameFromRefreshToken(string refreshToken)
        {
            // Your logic to extract username from refresh token
            // For demonstration purposes, assume username is stored in the refresh token
            return "Admin";
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RefreshTokenRequestModel
    {
        public string RefreshToken { get; set; }
    }
}
