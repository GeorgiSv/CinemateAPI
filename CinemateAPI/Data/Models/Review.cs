namespace CinemateAPI.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Review
    {
        public Review()
        {
            this.Comments = new List<Comment>();
            this.UsersLikes = new List<UserLikes>();
        }

        public int Id { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }

        public MovieDetails MovieDetails { get; set; }

        public string MovieDetailsId { get; set; }

        public IList<Comment> Comments { get; set; }

        public IList<UserLikes> UsersLikes { get; set; }
    }
}