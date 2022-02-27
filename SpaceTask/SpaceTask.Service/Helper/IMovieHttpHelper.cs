using SpaceTask.Data.DataModels;
using SpaceTask.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceTask.Service.Helper
{
    public interface IMovieHttpHelper
    {
        public Task<List<SearchResult>> GetMovieByName(string movieName);

        public Task<ImDbMovieRating> GetImDbRatingByMovieId(string movieId);

        public Task<ImDbMovieGenre> GetMovieGenre(string movieId);

        public Task<PosterDataDto> GetPosterByMovieId(string movieId);

        public Task<WikipediaDataDto> GetDescriptionByMovieId(string movieId);
    }
}
