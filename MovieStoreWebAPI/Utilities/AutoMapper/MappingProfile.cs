using AutoMapper;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Utilities.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<Movie, MovieViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.ActorMovieRelationship.Select(arm => arm.Actor.Name + " " + arm.Actor.Surname)));
        }
    }
}
