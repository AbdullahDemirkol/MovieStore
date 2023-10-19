using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.ActorMovieOperations.Query.QueryHandler.GetActorMovie
{
    public class GetActorMovieByIdQuery
    {

        public int ActorMovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorMovieByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ActorMovieViewModel Handle()
        {
            var actorMovie=_dbContext.ActorsMovies
                .Include(am=>am.Movie)
                .Include(am=>am.Actor)
                .FirstOrDefault(am=>am.Id==ActorMovieId);
            if (actorMovie is null)
            {
                throw new InvalidOperationException("Aktor-Film Iliskisi Bulunamadi.");
            }

            ActorMovieViewModel viewModel = _mapper.Map<ActorMovieViewModel>(actorMovie);

            return viewModel;
        }
    }
}
