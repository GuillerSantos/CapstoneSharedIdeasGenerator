using CapstoneIdeaGenerator.Client.Models.DTOs;

namespace CapstoneIdeaGenerator.Client.Services.Contracts
{
    public interface IAdminService
    {
        Task<Response> LoginAsync(AdminLoginDTO request);

        Task<AdminGetByEmailDTO> GetAdminByEmail(string email);

        Task<AdminDTO> Register(AdminRegisterDTO request);

        Task RemoveAdmin(string email);

        Task<Response> ForgotPassword(AdminForgotPasswordDTO request);

        Task<string> ResetPassword(AdminPasswordResetDTO request);

        Task<IEnumerable<AdminAccountDTO>> GetAllAccountsAsync();
    }
}
