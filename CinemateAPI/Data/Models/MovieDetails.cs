namespace CinemateAPI.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class MovieDetails
    {
        public MovieDetails()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Reviews = new List<Review>();
            this.UserFavourites = new List<UserFavourites>();
        }

        public string Id { get; set; }

        public string MovideDbId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string  HomePage { get; set; }

        public DateTime CreationDate { get; set; }

        public IList<Review> Reviews { get; set; }

        public IList<UserFavourites> UserFavourites { get; set; }
    }
}
