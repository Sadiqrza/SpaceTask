using SpaceTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpaceTask.Service.Repositoriy.Interface
{
    public interface IWatchlistRepository
    {
        public Task AddMovieToWatchlist(Watchlist watchlist);
        public Task UpdateWatchlistByMovie(Watchlist watchlist);
        public List<Watchlist> GetWatchlistByUserId(int userId);
        public Task<Watchlist> GetUserWatchlistByMovieName(Watchlist watchlist);
        public IEnumerable<Watchlist> Get(Expression<Func<Watchlist, bool>> expression);
    }
}
