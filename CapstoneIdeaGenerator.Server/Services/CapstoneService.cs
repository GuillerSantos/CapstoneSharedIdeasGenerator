using CapstoneIdeaGenerator.Server.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CapstoneIdeaGenerator.Server.DbContext;
using System.Threading.Tasks;
using System.Collections.Generic;
using CapstoneIdeaGenerator.Server.Models;

namespace CapstoneIdeaGenerator.Server.Services
{
    public class CapstoneService : ICapstoneServices
    {
        private readonly WebAppDbContext dbContext;

        public CapstoneService
            (
                WebAppDbContext dbContext
            )
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<Capstones>> GetAllCapstones()
        {
            return await dbContext.Capstones.ToListAsync();
        }


        public async Task<IEnumerable<Capstones>> GetFilteredCapstones(string? query)
        {
            var capstones = await dbContext.Capstones
                .Where(c => EF.Functions.Like(c.Title, $"%{query}%"))
                .ToListAsync();

            return capstones;
        }


        public async Task<Capstones> GetCapstonesById(int id)
        {
            return await dbContext.Capstones.FindAsync(id);
        }


        public async Task<Capstones> AddCapstones([FromBody] Capstones capstones)
        {
            dbContext.Capstones.Add(capstones);
            await dbContext.SaveChangesAsync();
            return capstones;
        }


        public async Task<Capstones> UpdateCapstones(int id, [FromBody] Capstones capstones)
        {
            var existingCapstone = await dbContext.Capstones.FirstOrDefaultAsync(c => c.CapstoneId == id);

            existingCapstone!.Title = capstones.Title;
            existingCapstone.Description = capstones.Description;
            existingCapstone.Categories = capstones.Categories;
            existingCapstone.CreatedBy = capstones.CreatedBy;
            existingCapstone.ProgLanguages = capstones.ProgLanguages;
            existingCapstone.Databases = capstones.Databases;
            existingCapstone.Frameworks = capstones.Frameworks;
            existingCapstone.ProjectType = capstones.ProjectType;

            await dbContext.SaveChangesAsync();
            return existingCapstone;
        }


        public async Task<bool> RemoveCapstones(int id)
        {
            var capstone = await dbContext.Capstones.FindAsync(id);

            if (capstone == null)
            {
                return false;
            }

            dbContext.Capstones.Remove(capstone);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
