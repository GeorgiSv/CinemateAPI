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
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReviewService reviewService;

        public ReviewController(IHttpContextAccessor httpContextAccessor, IReviewService reviewService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.reviewService = reviewService;
        }

        [HttpGet]
        [Route(nameof(GetAllReviews))]
        public async Task<IList<ReviewResponseModel>> GetAllReviews()
            => await this.reviewService.GetAllReviews();

        [HttpGet]
        [Route(nameof(GetReviewById))]
        public async Task<ReviewResponseModel> GetReviewById(string id)
            => await this.reviewService.GetReviewById(id);

        [HttpPost]
        [Route(nameof(CreateReview))]
        public async Task<string> CreateReview(CreateReviewRequestModel input)
        {
            var user = this.httpContextAccessor.HttpContext?.User;
            Console.WriteLine(user.Identity.Name);

            // Validate input data here

            var result = await this.reviewService.CreateReview(input);

            return result;
        }
    }
}
