using AutoMapper;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.RequestCommandModel;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryViewModel;
using MovieStoreWebAPI.Application.ActorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.CustomerOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Application.DirectorOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.DirectorOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Application.MovieOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Application.OrderOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Utilities.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //Actor
            CreateMap<Actor, ActorViewModel>()
                .ForMember(dest => dest.ActorFullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname))
                .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(m => m.Movie.Title)));
            CreateMap<CreateActorModel, Actor>();

            //Director
            CreateMap<Director, DirectorViewModel>()
                .ForMember(dest => dest.DirectorFullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname))
                .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(m => m.Title)));
            CreateMap<CreateDirectorModel, Director>();

            //Genre
            CreateMap<Genre, GenreViewModel>();
            CreateMap<CreateGenreModel, Genre>();

            //Movie
            CreateMap<Movie, MovieViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors.Select(arm => arm.Actor.Name + " " + arm.Actor.Surname)))
                .ForMember(dest => dest.YearOfMovie, opt => opt.MapFrom(src => src.YearOfMovie.Year.ToString()));
            CreateMap<CreateMovieModel, Movie>();

            //Actor-Movie
            CreateMap<ActorMovie, ActorMovieViewModel>()
                .ForMember(dest => dest.ActorName, opt => opt.MapFrom(src => src.Actor.Name + " " + src.Actor.Surname))
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title));
            CreateMap<CreateActorMovieModel, ActorMovie>();

            //Order
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name + " " + src.Customer.Surname))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest=>dest.TransactionTime,opt=>opt.MapFrom(src=>src.TransactionTime.ToString("dd/MM/yyyy HH:mm:ss")));
            CreateMap<CreateOrderModel, Order>()
                .ForMember(dest => dest.TransactionTime, opt => opt.MapFrom(src => DateTime.Now));

            //Customer
            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));

        }
    }
}
