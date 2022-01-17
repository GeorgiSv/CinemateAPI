namespace CinemateAPI.Data.Models
{
    using System;

    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }

        public Review Review { get; set; }

        public int ReviewId { get; set; }
    }
}