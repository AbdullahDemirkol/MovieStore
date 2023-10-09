using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovie
{
    public class GetMovieByIdQuery
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMovieByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public MovieViewModel Handle()
        {
            var movie=_dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .Include(m => m.ActorMovieRelationship).ThenInclude(arm => arm.Actor)
                .FirstOrDefault(m=>m.Id==MovieId);

            if (movie is null)
            {
                throw new InvalidOperationException("Film Bulunamadı.");
            }

            MovieViewModel movieViewModel=_mapper.Map<MovieViewModel>(movie);

            return movieViewModel;
        }

    }
}
