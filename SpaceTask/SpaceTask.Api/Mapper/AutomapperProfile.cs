using AutoMapper;
using SpaceTask.Data.DataModels;

namespace SpaceTask.Api.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<SearchResult, Movie>();
        }
    }
}
