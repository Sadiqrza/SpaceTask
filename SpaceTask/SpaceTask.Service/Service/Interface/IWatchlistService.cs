using SpaceTask.Data.DataModels;
using SpaceTask.Data.Dtos;
using SpaceTask.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceTask.Service.Service.Interface
{
    public interface IWatchlistService
    {
        public Task<List<UserMovieDto>> GetUserWatchlistMovies(int userId);

        public Task<Watchlist> UpdateMovieAsWatched(UserMovie model);

        public Task<MovieDto> AddMovieToUserWatchlist(UserMovie model);

        public Task<List<ImDbMovieRating>> GetMostRatedUnwatchedWatchlistMovies(int userId);

        public IEnumerable<Watchlist> GetUserWatchlist(int userId);
    }
}
