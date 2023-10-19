using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre=_dbContext.Genres.FirstOrDefault(g=>g.Id==GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Film Turu Bulunamadi.");
            }

            bool hasMovie=_dbContext.Movies.Any(m=>m.GenreId==GenreId);
            if (hasMovie)
            {
                throw new InvalidOperationException("Film Turu Ait Filmler Bulundugu Icin Film Turu Silinemedi.");
            }

            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }

    }
}
