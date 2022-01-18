namespace CinemateAPI.Features.Reviews
{
    using CinemateAPI.Data;
    using CinemateAPI.Data.Models;
    using CinemateAPI.Features.Reviews.Models;
    using CinemateAPI.Infrastructure.MovieDb;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReviewService : IReviewService
    {
        private readonly CinemateDbContext db;
        private readonly MovieDbService movieDbService;

        public ReviewService(CinemateDbContext db, MovieDbService movieDbService)
        {
            this.db = db;
            this.movieDbService = movieDbService;
        }

        public async Task<bool> CreateReview(CreateReviewRequestModel input)
        {
            try
            {
                var movieDetailsId = await db.MoviesDetails
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

                var result = await db.Reviews.AddAsync(review);
                Console.WriteLine(result);

                await db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        private async Task<string> GetAndSaveExternallyMovieDetails(int movieDbId)
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
    }
}
