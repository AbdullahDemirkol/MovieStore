using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<MovieViewModel> Handle()
        {
            var movies = _dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .Include(m => m.Actors).ThenInclude(arm => arm.Actor)
                .Where(m => m.IsActive)
                .OrderBy(m => m.Id)
                .ToList();

            List<MovieViewModel> moviesViewModel = _mapper.Map<List<MovieViewModel>>(movies);

            return moviesViewModel;
        }
    }
}
