using Microsoft.AspNetCore.Mvc;
using CapstoneIdeaGenerator.Server.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    namespace CapstoneIdeaGenerator.Server.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class AdminController : ControllerBase
        {
            private readonly IAdminService adminService;

            public AdminController(IAdminService adminService)
            {
                this.adminService = adminService;
            }


            [HttpGet, Authorize(Roles = "Admin")]
            public async Task<ActionResult<string>> GetMe()
            {
                var userName = await adminService.GetMyName();
                return Ok(userName);
            }


            [HttpGet("accounts"), Authorize(Roles = "Admin")]
            public async Task<ActionResult<IEnumerable<AdminDTO>>> GetAllAccounts()
            {
                try
                {
                    var accounts = await adminService.GetAllAccounts();
                    return Ok(accounts);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }


            [HttpGet("getbyemail")]
            public async Task<IActionResult> GetAdminByEmail(string email)
            {
                try
                {
                    var admin = await adminService.GetAdminByEmail(email);
                    return Ok(admin);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }


            [HttpPost("register"), Authorize(Roles = "Admin")]
            public async Task<ActionResult<AdminDTO>> Register(AdminRegisterDTO request)
            {
                try
                {
                    var admin = await adminService.RegisterAdmin(request);
                    return Ok(admin);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Admin Already Exist"))
                    {
                        return BadRequest(new { Message = "Admin With This Name Or Email Already Exists"});
                    }

                    return BadRequest(new { ex.Message });
                }
            }


            [HttpDelete("remove/{email}")]
            public async Task<IActionResult> RemoveAdmin(string email)
            {
                try
                {
                    await adminService.RemoveAdmin(email);
                    return Ok(new { message = "Admin Removed Successfully" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }



            [HttpPost("login")]
            public async Task<ActionResult<string>> Login([FromBody] AdminLoginDTO request)
            {
                try
                {
                    var token = await adminService.LoginAdmin(request);
                    return Ok(token);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }


            [HttpPost("forgot-password")]
            public async Task<IActionResult> ForgotPassword([FromBody] AdminForgotPasswordDTO request)
            {
                try
                {
                    var token = await adminService.GeneratePasswordResetToken(request);

                    return Ok(new
                    {
                        Message = "Password Reset Token Generated",
                        Token = token
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }


            [HttpPost("reset-password")]
            public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO request)
            {
                try
                {
                    await adminService.ResetPassword(request.Token, request.NewPassword);
                    return Ok(new { Message = "Password Has Been Reset Successfully" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }
        }
    }
}
