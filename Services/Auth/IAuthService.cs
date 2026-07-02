using BookStoreAPI.DTOs.Auth;
using static BookStoreAPI.DTOs.Auth.RegisterDTO;

namespace BookStoreAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto, string role = "Customer");
        Task<AuthResponseDto> LoginAsync(LoginDto dto);

    }
}
