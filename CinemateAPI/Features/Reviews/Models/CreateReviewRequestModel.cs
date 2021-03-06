namespace CinemateAPI.Features.Reviews.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataValidations.Review;

    public class CreateReviewRequestModel
    {
        [Required]
        [MaxLength(MaxTitleLength)]
        [MinLength(MinTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(MaxContentLength)]
        [MinLength(MinContentLength)]
        public string Content { get; set; }

        [Required]
        public int MovieDbId { get; set; }
    }
}
