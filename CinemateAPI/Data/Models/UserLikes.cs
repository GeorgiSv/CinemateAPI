namespace CinemateAPI.Data.Models
{
    public class UserLikes
    {
        public Review Review { get; set; }

        public int ReviewId { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }
    }
}