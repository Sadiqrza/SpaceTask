using AutoMapper;
using SpaceTask.Data.DataModels;
using SpaceTask.Data.Dtos;
using SpaceTask.Data.Entities;
using SpaceTask.Service.Helper;
using SpaceTask.Service.Repositoriy.Interface;
using SpaceTask.Service.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceTask.Service.Service.Implementation
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IMovieService _movieService;
        private readonly IWatchlistRepository _watchlistRepository;
        private readonly IMovieHttpHelper _movieHttpHelper;
        public WatchlistService(IMovieService movieService, IWatchlistRepository watchlistRepository, IMovieHttpHelper movieHttpHelper)
        {
            _movieService = movieService;
            _watchlistRepository = watchlistRepository;
            _movieHttpHelper = movieHttpHelper;
        }

        public async Task<MovieDto> AddMovieToUserWatchlist(UserMovie model)
        {
            var movieResponse = await _movieService.GetMovieByName(model.MovieName);
            await _watchlistRepository.AddMovieToWatchlist(new Watchlist
            {
                MovieId = movieResponse.Movie.Id,
                MovieName = model.MovieName,
                UserId = model.UserId
            });
            movieResponse.Message = "Succesfully Added";
            return movieResponse;
        }
        public async Task<List<ImDbMovieRating>> GetMostRatedUnwatchedWatchlistMovies(int userId)
        {
            var userMovieDtos = (await GetUserWatchlistMovies(userId)).Where(e => !e.IsWatched);
            var tasks = userMovieDtos.Select(userMovieDto => _movieHttpHelper.GetImDbRatingByMovieId(userMovieDto.Movie.Id)).ToList();
            var imDbMovieRatings = (await Task.WhenAll(tasks)).ToList();
            var result = imDbMovieRatings.OrderByDescending(x => x.ImDbRating);
            return result.ToList();
        }

        public async Task<List<UserMovieDto>> GetUserWatchlistMovies(int userId)
        {
            var watchlist = _watchlistRepository.GetWatchlistByUserId(userId);
            var userMovies = watchlist.Select(w => new { w.MovieName, w.IsWatched }).ToList();
            var tasks = userMovies.Select(userMovie => _movieService.GetMovieByName(userMovie.MovieName)).ToList();
            var tasksResult = await Task.WhenAll(tasks);
            var movieDtos = tasksResult.Select(s => s.Movie).ToList();
            return userMovies.Select(userMovie => new UserMovieDto { Movie = movieDtos.FirstOrDefault(m => m.Title == userMovie.MovieName), IsWatched = userMovie.IsWatched, UserId = userId }).ToList();
        }

        public IEnumerable<Watchlist> GetUserWatchlist(int userId)
        {
            return _watchlistRepository.Get(w => w.UserId == userId);
        }

        public async Task<Watchlist> UpdateMovieAsWatched(UserMovie model)
        {
            var watchlist = await _watchlistRepository.GetUserWatchlistByMovieName(new Watchlist { UserId = model.UserId, MovieName = model.MovieName });
            watchlist.IsWatched = true;
            await _watchlistRepository.UpdateWatchlistByMovie(watchlist);
            return watchlist;
        }
    }
}
