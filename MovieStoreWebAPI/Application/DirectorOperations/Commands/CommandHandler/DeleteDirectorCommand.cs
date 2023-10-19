using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.DirectorOperations.Commands.CommandHandler
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var director=_dbContext.Directors
                .Include(d=>d.Movies)
                .FirstOrDefault(d=>d.Id==DirectorId);
            if (director is null)
            {
                throw new InvalidOperationException("Yonetmen Bulunamadı.");
            }
            if (director.Movies.Count()>0)
            {
                throw new InvalidOperationException("Yonetmenin Filmleri Bulundugu Icin Silinemedi.");
            }
            _dbContext.Directors.Remove(director);
            _dbContext.SaveChanges();
        }
    }
}
