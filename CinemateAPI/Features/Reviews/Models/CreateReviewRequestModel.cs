namespace CinemateAPI.Features.Reviews.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewRequestModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int MovieDbId { get; set; }
    }
}
