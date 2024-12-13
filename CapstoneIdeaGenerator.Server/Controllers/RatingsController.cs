using System;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Entities.Models;
using CapstoneIdeaGenerator.Server.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService ratingsService;

        public RatingsController(IRatingsService ratingsService)
        {
            this.ratingsService = ratingsService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitRating([FromBody] RatingRequestDTO ratingRequest)
        {
            try
            {
                var success = await ratingsService.SubmitRating(
                    ratingRequest.RatingValue,
                    ratingRequest.CapstoneId,
                    ratingRequest.UserId,
                    ratingRequest.Title
                );

                if (success)
                {
                    return Ok("Rating submitted successfully.");
                }
                else
                {
                    return BadRequest("Failed to submit rating.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetRatings()
        {
            try
            {
                var ratings = await ratingsService.GetAllRatings();
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("allDetailes"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRatingsDetailes()
        {
            try
            {
                var ratings = await ratingsService.GetAllRatingsDetailes();
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
