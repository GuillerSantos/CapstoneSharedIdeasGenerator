using CapstoneIdeaGenerator.Server.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using CapstoneIdeaGenerator.Server.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CapstoneController : ControllerBase
    {
        private readonly ICapstoneServices capstoneServices;

        public CapstoneController
            (
            ICapstoneServices capstoneServices
            )
        {
            this.capstoneServices = capstoneServices;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Capstones>>> GetAllCapstones()
        {
            try
            {
                var capstones = await capstoneServices.GetAllCapstones();
                if (capstones == null)
                { 
                    return NotFound("No capstone found");
                }

                return Ok(capstones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredCapstones([FromQuery] string query)
        {
            try
            {
                var capstones = string.IsNullOrWhiteSpace(query)
                    ? await capstoneServices.GetAllCapstones()
                    : await capstoneServices.GetFilteredCapstones(query);

                return Ok(capstones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server  Error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Capstones>> GetCapstoneById(int id)
        {
            var capstone = await capstoneServices.GetCapstonesById(id);

            return Ok(capstone);
        }


        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] Capstones capstones)
        {
            await capstoneServices.AddCapstones(capstones);

            return CreatedAtAction(nameof(GetCapstoneById), new { id = capstones.CapstoneId.ToString() }, capstones);
        }


        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCapstone(int id, Capstones capstones)
        {
            try
            {
                var existingCapstone = await capstoneServices.UpdateCapstones(id, capstones);

                return Ok(existingCapstone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveCapstone(int id)
        {
            try
            {
                var capstone = await capstoneServices.RemoveCapstones(id);
                await capstoneServices.RemoveCapstones(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
