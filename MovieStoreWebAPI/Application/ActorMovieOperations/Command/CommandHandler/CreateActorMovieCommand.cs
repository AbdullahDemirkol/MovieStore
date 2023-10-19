using AutoMapper;
using MovieStoreWebAPI.Application.ActorMovieOperations.Command.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler
{
    public class CreateActorMovieCommand
    {
        public CreateActorMovieModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public void Handle()
        {
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(am => am.ActorId == Model.ActorId && am.MovieId == Model.MovieId);
            
            if (actorMovie is not null)
            {
                throw new InvalidOperationException("Aktor-Film Iliskisi Bulunmaktadır.");
            }

            actorMovie = _mapper.Map<ActorMovie>(Model);

            _dbContext.ActorsMovies.Add(actorMovie);
            _dbContext.SaveChanges();
        }
    }
}
