using SpaceTask.Data.DataModels;
using System.Collections.Generic;

namespace SpaceTask.Data.Dtos
{
    public class PosterDataDto
    {
        public string IMDbId { get; set; }
        public string Title { get; set; }

        public List<PosterDataItem> Posters { get; set; }
    }
}
