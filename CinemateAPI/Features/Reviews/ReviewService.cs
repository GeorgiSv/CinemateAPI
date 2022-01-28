namespace CinemateAPI.Features.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using CinemateAPI.Data;
    using CinemateAPI.Data.Models;
    using CinemateAPI.Features.Reviews.Models;
    using CinemateAPI.Infrastructure.MovieDb;

    public class ReviewService : IReviewService
    {
        private readonly CinemateDbContext db;
        private readonly MovieDbService movieDbService;

        public ReviewService(CinemateDbContext db, MovieDbService movieDbService)
        {
            this.db = db;
            this.movieDbService = movieDbService;
        }

        public async Task<string> CreateReview(CreateReviewRequestModel input, string userId)
        {
            try
            {
                var movieDetailsId = await this.db.MoviesDetails
                    .Where(x => x.MovideDbId == input.MovieDbId.ToString())
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                if (movieDetailsId == null)
                {
                    movieDetailsId = await GetAndSaveExternallyMovieDetails(input.MovieDbId);
                }

                var review = new Review()
                {
                    AuthorId = userId,
                    Summary = input.Title,
                    Content = input.Content,
                    MovieDetailsId = movieDetailsId,
                    CreationDate = DateTime.Now
                };

                await this.db.Reviews.AddAsync(review);
                await db.SaveChangesAsync();

                return review.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return "Failed to create review!";
            }
        }

        public async Task<string> DeleteReview(string id, string userId)
        {
            var review = await GetByIdAndUserId(id, userId);

            if (review == null)
            {
                return "User cannot delete this review!";
            }

            this.db.Reviews.Remove(review);
            await this.db.SaveChangesAsync();

            return "Review deleted successfully!";
        }

        public async Task<IList<ReviewResponseModel>> GetAllReviews()
            => await this.db.Reviews
                   .Select(r => new ReviewResponseModel
                   {
                       Id = r.Id,
                       Title = r.Summary,
                       Author = r.Author.UserName,
                       CreatedDate = r.CreationDate,
                       Content = r.Content,
                       Votes = r.UsersLikes.Count,
                       MovieImageUrl = r.MovieDetails.ImageUrl,
                       MovieTitle = r.MovieDetails.Title
                   })
                   .ToListAsync();

        public async Task<ReviewResponseModel> GetReviewById(string id)
            => await this.db.Reviews
                .Where(r => r.Id == id)
                .Select(r => new ReviewResponseModel() 
                {
                    Id = r.Id,
                    Title = r.Summary,
                    Author = r.Author.UserName,
                    CreatedDate = r.CreationDate,
                    Content = r.Content,
                    Votes = r.UsersLikes.Count,
                    MovieImageUrl = r.MovieDetails.ImageUrl,
                    MovieTitle = r.MovieDetails.Title
                })
                .FirstOrDefaultAsync();

        private async Task<string> GetAndSaveExternallyMovieDetails(int movieDbId)
        {
            try
            {
                var externalMovieDetails = await this.movieDbService.GetExternalMovieById(movieDbId);

                var movieDetails = new MovieDetails()
                {
                    Title = externalMovieDetails.Title,
                    MovideDbId = externalMovieDetails.Id.ToString(), // TODO: update DB type to INT
                    ImageUrl = externalMovieDetails.Poster_Path,
                    HomePage = externalMovieDetails.HomePage,
                    Description = externalMovieDetails.Overview,
                    CreationDate = DateTime.Now
                };

                await db.MoviesDetails.AddAsync(movieDetails);
                await db.SaveChangesAsync();

                return movieDetails.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<Review> GetByIdAndUserId(string reviewId, string userId)
            => await this.db.Reviews
                        .Where(x => x.Id == reviewId && x.AuthorId == userId)
                        .FirstOrDefaultAsync();
    }
}
