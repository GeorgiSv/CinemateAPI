
namespace CinemateAPI.Features.Reviews
{
    using CinemateAPI.Data;
    using CinemateAPI.Data.Models;
    using CinemateAPI.Features.Reviews.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReviewController : ApiController
    {
        private readonly CinemateDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ReviewController(CinemateDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route(nameof(GetAllReviews))]
        public async Task<IList<ReviewResponseModel>> GetAllReviews()
        {
            var reviews = await db.Reviews
                .Select(r => new ReviewResponseModel
                {
                    Title = r.Summary,
                    Author = r.Author.UserName,
                    CreatedDate = r.CreationDate,
                    Content = r.Content,
                    Votes = r.UsersLikes.Count,
                    MovieImageUrl = r.MovieDetails.ImageUrl,
                    MovieTitle = r.MovieDetails.Title

                }).ToListAsync();

            return reviews;
        }

        [HttpPost]
        [Route(nameof(CreateReview))]
        public async Task<int> CreateReview(CreateReviewRequestModel input)
        {
            var user = this.httpContextAccessor.HttpContext?.User;

            Console.WriteLine(user.Identity.Name);

            var movie = await db.MoviesDetails
                .Where(x => x.Id == input.MovieId)
                .FirstOrDefaultAsync();

            if (movie == null)
            {

            }

            var review = new Review()
            {
                AuthorId = input.AuthorId,
                Summary = input.Title,
                Content = input.Content,
                MovieDetailsId = input.MovieId,
                CreationDate = DateTime.Now
            };



            var result = await db.Reviews.AddAsync(review);

            Console.WriteLine(result);

            await db.SaveChangesAsync();


            return 1;
        }
    }
}
