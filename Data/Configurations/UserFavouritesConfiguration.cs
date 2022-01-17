namespace CinemateAPI.Data.Configurations
{
    using CinemateAPI.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserFavouritesConfiguration : IEntityTypeConfiguration<UserFavourites>
    {
        public void Configure(EntityTypeBuilder<UserFavourites> builder)
        {
            builder.HasKey(k => new { k.UserId, k.MovieDetailsId });
        }
    }
}
