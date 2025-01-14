using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.Models;
using CapstoneIdeaGenerator.Server.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneratorController : ControllerBase
    {
        private readonly IGeneratorService generatorService;

        public GeneratorController
            (IGeneratorService generatorService)
        {
            this.generatorService = generatorService;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllCategories()
        {
            try
            {
                var categories = await generatorService.GetAllCategories();
                if (categories == null || !categories.Any())
                {
                    return NotFound("No Category Found");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error:{ex.Message}");
            }
        }

        [HttpGet("projectTypes")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllProjectTypes()
        {
            try
            {
                var projectTypes = await generatorService.GetAllProjectTypes();
                if (projectTypes == null || !projectTypes.Any())
                {
                    return NotFound("No Project type Found");
                }
                return Ok(projectTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("idea/{category}/{projectType}")]
        public async Task<ActionResult<Capstones>> GetByProjectTypeAndCategory(string category, string projectType)
        {
            try
            {
                var idea = await generatorService.GetByProjectTypeAndCategory(category, projectType);
                if (idea == null)
                {
                    return NotFound("No idea found for the selected category and project type.");
                }
                return Ok(idea);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
