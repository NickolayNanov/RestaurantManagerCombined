using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        public record LoginRequest(string Username, string Password);

        public record LoginResponse(
            string AccessToken,
            string TokenType,
            int ExpiresInSeconds,
            string UserId,
            string Username,
            string[] Roles);

        // Hardcoded "users"
        private static readonly List<(string UserId, string Username, string Password, string[] Roles)> Users =
        [
            ("u-1", "admin", "admin123", new[] { "Admin" }),
            ("u-2", "owner", "owner123", new[] { "Owner" }),
            ("u-3", "user",  "user123",  new[] { "User" }),
    ];

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = Users.FirstOrDefault(u =>
                string.Equals(u.Username, request.Username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == request.Password);

            if (user == default)
                return Unauthorized(new { message = "Invalid username/password" });

            var token = CreateJwt(user.UserId, user.Username, user.Roles, out var expiresInSeconds);

            return Ok(new LoginResponse(
                AccessToken: token,
                TokenType: "Bearer",
                ExpiresInSeconds: expiresInSeconds,
                UserId: user.UserId,
                Username: user.Username,
                Roles: user.Roles));
        }

        private string CreateJwt(string userId, string username, string[] roles, out int expiresInSeconds)
        {
            var jwt = _config.GetSection("Jwt");
            var issuer = jwt["Issuer"]!;
            var audience = jwt["Audience"]!;
            var key = jwt["Key"]!;
            var expiresMinutes = int.TryParse(jwt["ExpiresMinutes"], out var m) ? m : 120;

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(expiresMinutes);
            expiresInSeconds = (int)(expires - now).TotalSeconds;

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
