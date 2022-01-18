namespace CinemateAPI.Features.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using CinemateAPI.Data;
    using CinemateAPI.Data.Models;
    using CinemateAPI.Features.Reviews.Models;
    using System.Net.Http;

    public class ReviewController : ApiController
    {
        private readonly CinemateDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReviewService reviewService;

        public ReviewController(CinemateDbContext db, IHttpContextAccessor httpContextAccessor, IReviewService reviewService)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
            this.reviewService = reviewService;
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
            

            // Validate input data here

            var result = await this.reviewService.CreateReview(input);

            return 1;
        }
    }
}
