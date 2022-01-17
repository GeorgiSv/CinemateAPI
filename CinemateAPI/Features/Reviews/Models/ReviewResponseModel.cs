namespace CinemateAPI.Features.Reviews.Models
{
    using System;

    public class ReviewResponseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Votes { get; set; }

        public string Content { get; set; }

        public string MovieTitle { get; set; }

        public string MovieImageUrl { get; set; }
    }
}
