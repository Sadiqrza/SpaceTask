using SpaceTask.Data.Dtos;
using System.Threading.Tasks;

namespace SpaceTask.Service.Service.Interface
{
    public interface IMovieService
    {
        public Task<MovieDto> GetMovieByName(string movieName);
    }
}
