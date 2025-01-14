using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.Models.DTOs;
using CapstoneIdeaGenerator.Server.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogsController : ControllerBase
    {
        private readonly IActivityLogsService activityLogsService;
        private readonly IAdminService adminService;

        public ActivityLogsController(IActivityLogsService activityLogsService, IAdminService adminService)
        {
            this.activityLogsService = activityLogsService;
            this.adminService = adminService;
        }


        [HttpPost("log")]
        public async Task<IActionResult> RecordLogActivity([FromBody] ActivityLogsDTO logs)
        {
            try
            {
                var admin = await adminService.GetAdminByEmail(logs.Email);
                if (admin == null)
                {
                    return BadRequest(new { Message = "Admin not found. Please verify the email." });
                }

                await activityLogsService.RecordLogActivity(admin.AdminId, admin.Name, admin.Email, logs.Action, logs.Details);
                return Ok(new { Message = "Activity logged successfully." });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error logging activity: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while logging activity.", Error = ex.Message });
            }
        }


        [HttpGet("getlogs"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ActivityLogsDTO>>> GetActivityLogs()
        {
            var logs = await activityLogsService.GetAllActivityLogs();
            return Ok(logs);
        }
    }
}
