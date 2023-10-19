using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.ActorMovieOperations.Command.CommandHandler
{
    public class DeleteActorMovieCommand
    {
        public int ActorMovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteActorMovieCommand(IMovieStoreDbContext dbContext)
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

            _dbContext.ActorsMovies.Remove(actorMovie);
            _dbContext.SaveChanges();
        }

    }
}
