using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.DbContext;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.Models;
using CapstoneIdeaGenerator.Server.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CapstoneIdeaGenerator.Server.Services
{
    public class ActivityLogsService : IActivityLogsService
    {
        private readonly WebAppDbContext dbContext;

        public ActivityLogsService(WebAppDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }


        public async Task RecordLogActivity(int adminId, string name, string email, string action, string details)
        {
            try
            {
                var log = new ActivityLogs
                {
                    AdminId = adminId,
                    Email = email,
                    Name = name,
                    Action = action,
                    Details = details,
                    Timestamp = DateTime.UtcNow
                };

                dbContext.ActivityLogs.Add(log);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding activity logs: {ex.Message}");
                throw;
            }
        }



        public async Task<IEnumerable<ActivityLogsDTO>> GetAllActivityLogs()
        {
            var logs = await dbContext.ActivityLogs
                .Select(al => new ActivityLogsDTO
                {
                    AdminId = al.AdminId,
                    Name = al.Name,
                    Action = al.Action,
                    Details = al.Details,
                    Timestamp = al.Timestamp
                })
                .ToListAsync();

            return logs;
        }
    }
}
