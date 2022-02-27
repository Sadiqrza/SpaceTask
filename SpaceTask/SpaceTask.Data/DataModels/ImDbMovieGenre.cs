using System.Collections.Generic;

namespace SpaceTask.Data.DataModels
{
    public class ImDbMovieGenre
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public List<KeyValueItem> GenreList { get; set; }
    }

    public class KeyValueItem
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
