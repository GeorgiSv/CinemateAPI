namespace CinemateAPI.Features.Reviews
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CinemateAPI.Features.Reviews.Models;

    public interface IReviewService
    {
        Task<string> CreateReview(CreateReviewRequestModel input, string userId);

        Task<IList<ReviewResponseModel>> GetAllReviews();

        Task<ReviewResponseModel> GetReviewById(string id);

        Task<string> DeleteReview(string id, string userId);
    }
}
