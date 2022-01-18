//namespace CinemateAPI.Infrastructure
//{
//    using Microsoft.Extensions.Options;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Net.Http;
//    using System.Threading.Tasks;

//    public class MovieDbClient
//    {
//        private readonly HttpClient client;
//        private readonly MovieDbConfig movieDbConfig;

//        public MovieDbClient(IOptions<MovieDbConfig> movieDbConfig, IHttpClientFactory httpClientFactory)
//        {
//            this.movieDbConfig = movieDbConfig.Value;
//            this.client = httpClientFactory.CreateClient(this.movieDbConfig.ClientName);
//        }

//        public async Task GetMovieById(string movieId)
//        {
//            await this.client.GetAsync($"/movie/{movieId}?api_key=${this.movieDbConfig.ApiKey}");

//        }
//    }
//}
