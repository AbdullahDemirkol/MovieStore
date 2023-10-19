using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryHandler.GetActorsMovies
{
    public class GetActorsMoviesQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorsMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ActorMovieViewModel> Handle()
        {
            var actorsMovies = _dbContext.ActorsMovies
                .Include(am => am.Actor)
                .Include(am => am.Movie)
                .OrderBy(am=>am.Id).ToList();

            List<ActorMovieViewModel> viewModels =_mapper.Map<List<ActorMovieViewModel>>(actorsMovies);

            return viewModels;
        }
    }
}
