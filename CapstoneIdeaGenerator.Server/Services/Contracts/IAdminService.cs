using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using CapstoneIdeaGenerator.Server.Entities.DTOs;

namespace CapstoneIdeaGenerator.Server.Services.Contracts
{
    public interface IAdminService
    {
        Task<string> GetMyName();
        Task<AdminGetByEmailDTO> GetAdminByEmail(string email);
        Task<IEnumerable<AdminDTO>> GetAllAccounts();
        Task<AdminDTO> RegisterAdmin(AdminRegisterDTO request);
        Task<string> LoginAdmin(AdminLoginDTO request);
        Task ResetPassword(string token, string newPassword);
        Task<string> GeneratePasswordResetToken(AdminForgotPasswordDTO request);
        string CreateToken(Admins admin);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        Task RemoveAdmin(string email);
    }
}