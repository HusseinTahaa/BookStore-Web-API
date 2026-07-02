using BookStoreAPI.DTOs.Auth;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BookStoreAPI.DTOs.Auth.RegisterDTO;

namespace BookStoreAPI.Services.Auth
{
   public class AuthService : IAuthService
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        public AuthService(ILogger<AuthService> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _configuration = configuration;
            _logger = logger;
        }

            public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto, string role = "Customer")
            {
                var userExists = await _userManager.FindByEmailAsync(dto.Email);
                if (userExists != null)
                    return new AuthResponseDto { IsSuccess = false, Message = "User already exists!" };

                var user = new ApplicationUser
                {
                    Email = dto.Email,
                    UserName = dto.Email,
                    FullName = dto.FullName
                };

                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return new AuthResponseDto { IsSuccess = false, Message = $"User creation failed: {errors}" };
                }

                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));

                await _userManager.AddToRoleAsync(user, role);

                return new AuthResponseDto { IsSuccess = true, Message = "User created successfully!" };
            }

            public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                _logger.LogWarning($"Failed login attempt for email: {dto.Email}");
                return new AuthResponseDto { IsSuccess = false, Message = "Invalid email or password." };
            }


                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);
            _logger.LogInformation($"User {user.Email} logged in successfully.");

            return new AuthResponseDto
                {
                    IsSuccess = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    Message = "Login successful"
                };
            }

            private JwtSecurityToken GetToken(List<Claim> authClaims)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return token;
            }


        }
}
