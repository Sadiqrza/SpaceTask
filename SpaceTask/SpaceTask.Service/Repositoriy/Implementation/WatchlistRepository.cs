using Microsoft.EntityFrameworkCore;
using SpaceTask.Data;
using SpaceTask.Data.Entities;
using SpaceTask.Service.Repositoriy.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpaceTask.Service.Implementation
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly SpaceTaskDbContext _context;

        public WatchlistRepository(SpaceTaskDbContext context)
        {
            _context = context;
        }

        public async Task AddMovieToWatchlist(Watchlist watchlist)
        {
            await _context.Watchlists.AddAsync(watchlist);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateWatchlistByMovie(Watchlist watchlist)
        {
                _context.Watchlists.Update(watchlist);
                await _context.SaveChangesAsync();
        }
        public List<Watchlist> GetWatchlistByUserId(int userId)
        {
            return _context.Watchlists.AsNoTracking().Where(w => w.UserId == userId).ToList();
        }

        public async Task<Watchlist> GetUserWatchlistByMovieName(Watchlist watchlist)
        {
            return await _context.Watchlists.Where(w => w.UserId == watchlist.UserId && w.MovieName == watchlist.MovieName).FirstOrDefaultAsync();
        }

        public IEnumerable<Watchlist> Get(Expression<Func<Watchlist, bool>> expression)
        {
            return _context.Watchlists.Where(expression);
        }
    }
}
