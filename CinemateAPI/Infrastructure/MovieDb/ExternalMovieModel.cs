namespace CinemateAPI.Infrastructure.MovieDb
{
    public class ExternalMovieModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Poster_Path { get; set; }

        public string HomePage { get; set; }

        public string Overview { get; set; }
    }
}