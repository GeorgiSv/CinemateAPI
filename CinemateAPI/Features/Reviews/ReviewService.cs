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

        public async Task<string> CreateReview(CreateReviewRequestModel input)
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
                    AuthorId = input.AuthorId,
                    Summary = input.Title,
                    Content = input.Content,
                    MovieDetailsId = movieDetailsId,
                    CreationDate = DateTime.Now
                };

                var result = await this.db.Reviews.AddAsync(review);
                Console.WriteLine(result);

                await db.SaveChangesAsync();

                return review.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<IList<ReviewResponseModel>> GetAllReviews()
        {
            var reviews = await this.db.Reviews
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

            return reviews;
        }

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
    }
}
