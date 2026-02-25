using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace RestaurantManager.Application.Handlers.Auth
{
    internal class AuthHandler(
        ILogger<AuthHandler> logger,
        IConfiguration configuration,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager) : IRequestHandler<AuthQuery, AuthResponse>
    {
        public async Task<AuthResponse> Handle(AuthQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                logger.LogError($"Not found user with email: {request.Email}");
                throw new ResourceNotFoundException(nameof(ApplicationUser), $"User with email: {request.Email} not found.");
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

            if (!signInResult.Succeeded)
            {
                logger.LogError($"Invalid password for user with email: {request.Email}");
                throw new InvalidOperationException($"Invalid password for user with email: {request.Email}.");
            }

            var roles = await userManager.GetRolesAsync(user);

            var (accessToken, expires) = CreateJwt(user, roles);

            return new AuthResponse(accessToken, expires);
        }

        private (string accessToken, DateTime expires) CreateJwt(ApplicationUser user, IEnumerable<string> roles)
        {
            var jwt = configuration.GetSection("Jwt");

            var issuer = jwt["Issuer"];
            var audience = jwt["Audience"];
            var key = jwt["Key"];

            var expiresMinutes = int.TryParse(jwt["ExpiresMinutes"], out var m) ? m : 120;

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(expiresMinutes);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
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

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }
    }
}
