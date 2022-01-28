namespace CinemateAPI.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataValidations.Review;

    public class Review
    {
        public Review()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new List<Comment>();
            this.UsersLikes = new List<UserLikes>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Summary { get; set; }

        [Required]
        [MaxLength(MaxContentLength)]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public User Author { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public MovieDetails MovieDetails { get; set; }

        [Required]
        public string MovieDetailsId { get; set; }

        public IList<Comment> Comments { get; set; }

        public IList<UserLikes> UsersLikes { get; set; }
    }
}