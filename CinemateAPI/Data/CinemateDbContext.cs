namespace CinemateAPI.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using CinemateAPI.Data.Models;

    public class CinemateDbContext : IdentityDbContext<User>
    {
        public CinemateDbContext(DbContextOptions<CinemateDbContext> options)
            : base(options)
        { }

        public DbSet<MovieDetails> MoviesDetails { get; set; }

        public DbSet<UserLikes> UsersLikes { get; set; }

        public DbSet<UserFavourites> UsersFavourites { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Comment> Comment { get; set; }

        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);
        }
    }
}
