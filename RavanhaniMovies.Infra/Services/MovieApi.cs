using Microsoft.Extensions.Configuration;
using RavanhaniMovies.Domain.Interfaces;
using RavanhaniMovies.Domain.Models;
using RestSharp;
using System.Text.Json;

namespace RavanhaniMovies.Infra.Services
{
    public class MovieApi : IMovieApi
    {
        private IConfiguration _configuration;

        public MovieApi(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Movie>> GetList(string listId)
        {
            string? apiKey = _configuration["DB_Movie:Api_Key"];
            var options = new RestClientOptions($"https://api.themoviedb.org/3");
            var client = new RestClient(options);
            var request = new RestRequest($"list/{listId}");
            request.AddQueryParameter("language", "en-US");
            request.AddQueryParameter("page", "1");
            request.AddQueryParameter("api_key", apiKey);
            request.AddHeader("accept", "application/json");
            var response = await client.GetAsync(request);

            MovieList? movieList = JsonSerializer.Deserialize<MovieList>(response.Content);

            if (movieList != null)
            {
                return movieList.Items;
            }

            return Enumerable.Empty<Movie>();

        }
    }
}
