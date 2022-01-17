namespace CinemateAPI.Data.Models
{
    public class UserFavourites
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public MovieDetails MovieDetails { get; set; }

        public string MovieDetailsId { get; set; }
    }
}
