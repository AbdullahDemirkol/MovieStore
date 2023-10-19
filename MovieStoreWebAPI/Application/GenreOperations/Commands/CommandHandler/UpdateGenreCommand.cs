using AutoMapper;
using MovieStoreWebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre=_dbContext.Genres.FirstOrDefault(g=>g.Id==GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Film Turu Bulunamadı.");
            }
            var hasGenre = _dbContext.Genres.Any( g => g.Name == Model.Name && g.Id!=GenreId);
            if (hasGenre)
            {
                throw new InvalidOperationException("Verilen Film Turu Ismi Baska Bir Film Türünde Bulunmaktadır.");
            }

            bool hasMovie=_dbContext.Movies.Any(m=>m.GenreId==GenreId);
            if (Model.IsActive == false && hasMovie)
            {
                throw new InvalidOperationException("Film Turune Ait Film Bulundugu Icin Statusu Pasif Yapılamaz.");
            }

            genre.Name = !string.IsNullOrEmpty(Model.Name) ? Model.Name : genre.Name;
            genre.IsActive = !bool.Equals(genre.IsActive, Model.IsActive) ? Model.IsActive : genre.IsActive;

            _dbContext.SaveChanges();
        }
    }
}
