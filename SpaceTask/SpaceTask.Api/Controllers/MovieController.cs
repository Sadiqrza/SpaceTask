using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceTask.Data.Dtos;
using SpaceTask.Service.Service.Interface;
using System;
using System.Threading.Tasks;

namespace SpaceTask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger _log;
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService, ILogger log)
        {
            _movieService = movieService;
            _log = log;
        }


        [HttpGet]
        public async Task<ActionResult<MovieDto>> GetMovieByName(string movieName)
        {
            try
            {
                var result = await _movieService.GetMovieByName(movieName);

                if (result.Movie == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return BadRequest();
            }
        }
    }
}
