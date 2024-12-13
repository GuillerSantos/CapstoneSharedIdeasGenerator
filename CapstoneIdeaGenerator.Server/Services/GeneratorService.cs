using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.DbContext;
using CapstoneIdeaGenerator.Server.Entities.Models;
using CapstoneIdeaGenerator.Server.Services.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CapstoneIdeaGenerator.Server.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly WebAppDbContext dbContext;

        public GeneratorService(WebAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> GetAllCategories()
        {
            return await dbContext.Capstones
                .Select(c => c.Categories)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllProjectTypes()
        {
            return await dbContext.Capstones
                .Select(c => c.ProjectType)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Capstones> GetByProjectTypeAndCategory(string category, string projectType)
        {
            var capstoneIdea = await dbContext.Capstones
                .Where(c => c.Categories == category && c.ProjectType == projectType)
                .ToListAsync();

            if (!capstoneIdea.Any())
            {
                return null;
            }

            var random = new Random();
            var randomIndex = random.Next(0, capstoneIdea.Count);

            var selectedCapstone = capstoneIdea[randomIndex];
            return selectedCapstone;
        }
    }
}
