using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.ActorOperations.Commands.CommandHandler
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var actor=_dbContext.Actors
                .Include(a=>a.Movies)
                .FirstOrDefault(a=>a.Id==ActorId);
            if (actor is null)
            {
                throw new InvalidOperationException("Aktor Bulunamadi.");
            }
            if (actor.Movies.Count()>0)
            {
                throw new InvalidOperationException("Aktorun Oynadigi Filmler Bulundugu Icin Silinemedi.");
            }

            _dbContext.Actors.Remove(actor);
            _dbContext.SaveChanges();
        }
    }
}
