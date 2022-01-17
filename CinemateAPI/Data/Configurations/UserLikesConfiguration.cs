namespace CinemateAPI.Data.Configurations
{
    using CinemateAPI.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserLikesConfiguration : IEntityTypeConfiguration<UserLikes>
    {
        public void Configure(EntityTypeBuilder<UserLikes> builder)
        {
            builder.HasKey(k => new { k.AuthorId, k.ReviewId });
        }
    }
}
