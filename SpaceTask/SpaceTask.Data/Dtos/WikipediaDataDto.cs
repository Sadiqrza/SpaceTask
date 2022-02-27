using SpaceTask.Data.DataModels;

namespace SpaceTask.Data.Dtos
{
    public class WikipediaDataDto
    {
        public string IMDbId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public WikipediaDataPlot PlotShort { get; set; }
    }
}
