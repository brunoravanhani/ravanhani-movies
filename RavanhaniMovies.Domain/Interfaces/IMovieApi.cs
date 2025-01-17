using RavanhaniMovies.Domain.Models;

namespace RavanhaniMovies.Domain.Interfaces
{
    public interface IMovieApi
    {
        Task<IEnumerable<Movie>> GetList(string listId);
    }
}
