namespace CinemateAPI.Features.Reviews
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using CinemateAPI.Features.Reviews.Models;
    using CinemateAPI.Features.User;

    public class ReviewController : ApiController
    {
        private readonly ICurrentUserService userService;
        private readonly IReviewService reviewService;

        public ReviewController(ICurrentUserService userService, IReviewService reviewService)
        {
            this.userService = userService;
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
            var userId = this.userService.GetId();
            var result = await this.reviewService.CreateReview(input, userId);

            return result;
        }


        //[HttpPut]
        //[Route(nameof(UpdateReview))]
        //public async Task<string> UpdateReview(CreateReviewRequestModel input)
        //{
        //    // Validate input data here
        //    var result = await this.reviewService.edit(input);

        //    return result;
        //}

        [HttpDelete]
        [Route(nameof(DeleteReview))]
        public async Task<string> DeleteReview(string id)
        {
            var userId = this.userService.GetId();
            var result = await this.reviewService.DeleteReview(id, userId);

            return result;
        }
    }
}
