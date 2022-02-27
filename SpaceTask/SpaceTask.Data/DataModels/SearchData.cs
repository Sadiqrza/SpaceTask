using SpaceTask.Data.DataModels;
using System.Collections.Generic;

namespace SpaceTask.Data.DataModels
{
    public class SearchData
    {
        public string SearchType { get; set; }

        public string Expression { get; set; }

        public List<SearchResult> Results { get; set; }

        public string ErrorMessage { get; set; }
    }
}
