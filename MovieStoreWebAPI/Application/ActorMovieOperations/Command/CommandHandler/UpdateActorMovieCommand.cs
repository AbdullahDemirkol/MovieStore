using MovieStoreWebAPI.Application.ActorMovieOperations.Command.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler
{
    public class UpdateActorMovieCommand
    {
        public int ActorMovieId { get; set; }
        public UpdateActorMovieModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateActorMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(am => am.Id == ActorMovieId);
            if (actorMovie is null)
            {
                throw new InvalidOperationException("Aktor-Film Iliskisi Bulunamadi.");
            }

            var hasActor = _dbContext.Actors.Any(a => a.Id == Model.ActorId);
            if (!hasActor)
            {
                throw new InvalidOperationException("Aktör Bulunamadi.");
            }

            var hasMovie = _dbContext.Movies.Any(m => m.Id == Model.MovieId);
            if (!hasMovie)
            {
                throw new InvalidOperationException("Film Bulunamadi.");
            }

            var hasActorMovie = _dbContext.ActorsMovies.Any(am => am.MovieId == Model.MovieId
            && am.ActorId == Model.ActorId && am.Id != ActorMovieId);
            if (hasActorMovie)
            {
                throw new InvalidOperationException("Zaten Aktor-Film Iliskisi Bulunmaktadir.");
            }

            actorMovie.ActorId = Model.ActorId;
            actorMovie.MovieId = Model.MovieId;


            _dbContext.SaveChanges();
        }

    }
}
