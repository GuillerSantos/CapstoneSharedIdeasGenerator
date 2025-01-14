using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneIdeaGenerator.Server.DbContext;
using CapstoneIdeaGenerator.Server.Models;
using CapstoneIdeaGenerator.Server.Models.DTOs;
using CapstoneIdeaGenerator.Server.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CapstoneIdeaGenerator.Server.Services
{
    public class RatingsService : IRatingsService
    {
        private readonly WebAppDbContext dbContext;

        public RatingsService (WebAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> SubmitRating(int ratingValue, int capstoneId, string userId, string title)
        {
            if (ratingValue < 1 || ratingValue > 5)
            {
                throw new ArgumentException("Rating value must be between 1 and 5.");
            }

            var capstone = await dbContext.Capstones.FindAsync(capstoneId);
            if (capstone == null)
            {
                throw new ArgumentException("Capstone with the given ID not found.");
            }

            var existingRating = await dbContext.Ratings
                .Where(r => r.CapstoneId == capstoneId && r.UserId == userId)
                .FirstOrDefaultAsync();

            if (existingRating != null)
            {
                existingRating.RatingValue = ratingValue;
                existingRating.Title = title;
                existingRating.RatedOn = DateTime.UtcNow;

                dbContext.Ratings.Update(existingRating);
            }
            else
            {
                var newRating = new Ratings
                {
                    CapstoneId = capstoneId,
                    RatingValue = ratingValue,
                    Title = title,
                    UserId = userId,
                    RatedOn = DateTime.UtcNow
                };

                dbContext.Ratings.Add(newRating);
            }

            await dbContext.SaveChangesAsync();

            return true;
        }



        public async Task<IEnumerable<RatingRequestDTO>> GetAllRatings()
        {
            var ratings = await dbContext.Ratings
                .Join(dbContext.Capstones,
                      rating => rating.CapstoneId,
                      capstone => capstone.CapstoneId,
                      (rating, capstone) => new RatingRequestDTO
                      {
                          Title = capstone.Title,
                          RatingValue = rating.RatingValue
                      })
                .ToListAsync();

            return ratings;
        }

        public async Task<IEnumerable<Ratings>> GetAllRatingsDetailes()
        {
            var ratings = await dbContext.Ratings
                .Join(dbContext.Capstones,
                      rating => rating.CapstoneId,
                      capstone => capstone.CapstoneId,
                      (rating, capstone) => new Ratings
                      {
                          RatingId = rating.RatingId,
                          CapstoneId = capstone.CapstoneId,
                          UserId = rating.UserId,
                          Title = capstone.Title,
                          RatingValue = rating.RatingValue,
                          RatedOn = rating.RatedOn,
                      })
                .ToListAsync();

            return ratings;
        }
    }
}
