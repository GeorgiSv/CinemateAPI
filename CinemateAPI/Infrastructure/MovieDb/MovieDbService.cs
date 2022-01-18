namespace CinemateAPI.Infrastructure.MovieDb
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class MovieDbService
    {
        private readonly MovieDbConfig movieDbConfig;
        private readonly HttpClient movieDbClient;

        public MovieDbService(IOptions<MovieDbConfig> movieDbCOnfig, IHttpClientFactory httpClientFactory)
        {
            this.movieDbConfig = movieDbCOnfig.Value;
            this.movieDbClient = httpClientFactory.CreateClient(this.movieDbConfig.ClientName);
        }

        public async Task<ExternalMovieModel> GetExternalMovieById(int id)
        {
            try
            {
                var response = await this.movieDbClient.GetAsync("movie/" + id + "?api_key=" + this.movieDbConfig.ApiKey);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Failed to fetch external movie from MovieDbApi! StatusCode: " + response.StatusCode);
                    return null;
                }

                var resultAsString = await response.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject<ExternalMovieModel>(resultAsString);

                return deserialized;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to fetch external movie from MovieDbApi! Error: " + ex.Message);
                throw;
            }
  
        }
    }
}
