namespace CinemateAPI.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.UsersLikes = new List<UserLikes>();
            this.Comments = new List<Comment>();
            this.Favourites = new List<UserFavourites>();
            this.Reviews = new List<Review>();
        }

        public string ProfilePicture { get; set; }

        public IList<UserLikes> UsersLikes { get; set; }

        public IList<Comment> Comments { get; set; }

        public IList<Review> Reviews { get; set; }

        public IList<UserFavourites> Favourites { get; set; }
    }
}
