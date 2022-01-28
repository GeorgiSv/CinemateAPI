namespace CinemateAPI.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataValidations.Review;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxContentLength)]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public User Author { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public Review Review { get; set; }

        [Required]
        public int ReviewId { get; set; }
    }
}