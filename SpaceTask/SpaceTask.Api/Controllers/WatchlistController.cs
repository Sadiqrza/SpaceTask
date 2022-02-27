using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceTask.Data.DataModels;
using SpaceTask.Data.Dtos;
using SpaceTask.Data.Entities;
using SpaceTask.Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceTask.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;
        private readonly ILogger _logger;

        public WatchlistController(IWatchlistService watchlistService, ILogger logger)
        {
            _watchlistService = watchlistService;
            _logger = logger;
        }

        /// <summary>
        /// Add movie to the user's watchlist
        /// </summary>
        [HttpPost("addMovie")]
        public async Task<ActionResult<MovieDto>> AddMovieToUserWatchlist([FromBody] UserMovie model)
        {
            try
            {
                var result = await _watchlistService.AddMovieToUserWatchlist(model);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get all movies from user's watchlist
        /// </summary>
        [HttpGet("movies/{userId}")]
        public async Task<ActionResult<List<UserMovieDto>>> GetUserWatchlistMovies(int userId)
        {
            try
            {
                var result = await _watchlistService.GetUserWatchlistMovies(userId);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Set the movie as watched for particular user
        /// </summary>
        [HttpPatch("updateMovieAsWatched")]
        public async Task<ActionResult<Watchlist>> UpdateMovieAsWatched([FromBody] UserMovie model)
        {
            try
            {
                var result = await _watchlistService.UpdateMovieAsWatched(model);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
    }
}
