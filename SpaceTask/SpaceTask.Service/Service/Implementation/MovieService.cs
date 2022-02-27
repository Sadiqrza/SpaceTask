using AutoMapper;
using SpaceTask.Data.DataModels;
using SpaceTask.Data.Dtos;
using SpaceTask.Service.Helper;
using SpaceTask.Service.Service.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceTask.Service.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IMovieHttpHelper _movieHttpHelper;

        public MovieService(IMapper mapper, IMovieHttpHelper movieHttpHelper)
        {
            _mapper = mapper;
            _movieHttpHelper = movieHttpHelper;
        }

        public async Task<MovieDto> GetMovieByName(string movieName)
        {
            var searchResult = await _movieHttpHelper.GetMovieByName(movieName);

            var result = searchResult.FirstOrDefault(m => String.Equals(m.Title, movieName, StringComparison.CurrentCultureIgnoreCase));

            if (result == null)
            {
                return new MovieDto { Message = "Can not find movie" };
            }

            var movie = _mapper.Map<Movie>(result);

            return new MovieDto
            {
                Message = "Successful",
                Movie = movie
            };
        }
    }
}
