using Microsoft.Extensions.Options;
using SpaceTask.Data.DataModels;
using SpaceTask.Data.Entities;
using SpaceTask.Service.Configuration;
using SpaceTask.Service.Helper;
using SpaceTask.Service.Repositoriy.Interface;
using SpaceTask.Service.Service.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace SpaceTask.Service.JobService
{
    public class JobLogic : IJobLogic
    {
        private readonly IWatchlistService _watchlistService;
        private readonly IEmailService _emailService;
        private readonly IMovieHttpHelper _movieHttpHelper;
        private readonly ApiOptions _apiOptions;
        private readonly IWatchlistRepository _watchlistRepository;

        public JobLogic(IWatchlistService watchlistService,
            IEmailService emailService,
            IMovieHttpHelper movieHttpHelper,
            IOptions<ApiOptions> options,
            IWatchlistRepository watchlistRepository)
        {
            _watchlistService = watchlistService;
            _emailService = emailService;
            _movieHttpHelper = movieHttpHelper;
            _watchlistRepository = watchlistRepository;
            _apiOptions = options.Value;
        }

        public async Task JobLogicMehtod()
        {
            var userId = 1;
            var unwatchedMostRateOrderedMovies = await _watchlistService.GetMostRatedUnwatchedWatchlistMovies(userId);
            if (unwatchedMostRateOrderedMovies.Count > 3)
            {
                var userWatchlistToOffer = _watchlistService.GetUserWatchlist(userId).Where(w => !w.IsWatched)
                    .Where(m => DateTime.Now - m.SendingMailTime > TimeSpan.FromDays(30));
                var result = unwatchedMostRateOrderedMovies.FirstOrDefault(m => m.Id == userWatchlistToOffer.FirstOrDefault()?.MovieId);
                var poster = await _movieHttpHelper.GetPosterByMovieId(result?.Id);
                var description = await _movieHttpHelper.GetDescriptionByMovieId(result?.Id);

                var emailInfo = new EmailInfo
                {
                    Password = _apiOptions.Password,
                    FromEmail = _apiOptions.FromEmail,
                    Subject = "Recommended film for watching",
                    ToEmail = _apiOptions.ToEmail,
                    Message = $"Movie:{result?.Title} Rating: {result?.ImDbRating}, Poster: {poster.Posters.FirstOrDefault()}, Description: {description.PlotShort}"
                };

                await _emailService.SendEmailAsync(emailInfo);
                var watchlist = await _watchlistRepository.GetUserWatchlistByMovieName(new Watchlist { UserId = userId, MovieName = result?.Title });
                if (watchlist != null)
                {
                    watchlist.SendingMailTime = DateTime.Now;
                    await _watchlistRepository.UpdateWatchlistByMovie(watchlist);
                }
            }
        }
    }
}
