namespace CinemateAPI.Features.Reviews.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataValidations.Review;

    public class UpdateReviewRequestModel
    {

        [Required]
        [MaxLength(MaxContentLength)]
        [MinLength(MinContentLength)]
        public string Content { get; set; }
    }
}
