namespace CinemateAPI.Features.Reviews
{
    using CinemateAPI.Features.Reviews.Models;
    using System.Threading.Tasks;

    public interface IReviewService
    {
        public Task<bool> CreateReview(CreateReviewRequestModel input);
    }
}
