using SpaceTask.Data.DataModels;

namespace SpaceTask.Data.Dtos
{
    public class UserMovieDto
    {
        public Movie Movie { get; set; }

        public int UserId { get; set; }

        public bool IsWatched { get; set; }
    }
}
