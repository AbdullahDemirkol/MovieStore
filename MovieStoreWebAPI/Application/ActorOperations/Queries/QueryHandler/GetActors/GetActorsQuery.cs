using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.ActorOperations.Queries.QueryHandler.GetActors
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<ActorViewModel> Handle()
        {
            var movies = _dbContext.Actors
                .Include(a => a.Movies).ThenInclude(m => m.Movie)
                .OrderBy(a => a.Id)
                .ToList();
            List<ActorViewModel> viewModel = _mapper.Map<List<ActorViewModel>>(movies);
            return viewModel;
        }
    }
}
