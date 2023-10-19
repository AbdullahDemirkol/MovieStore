using MovieStoreWebAPI.Application.MovieOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.DataAccess.Concrete;

namespace MovieStoreWebAPI.Application.MovieOperations.Commands.CommandHandler
{
    public class UpdateMovieCommand
    {
        public int MovieId { get; set; }
        public UpdateMovieModel Model { get; set; }
        private IMovieStoreDbContext _dbContext;

        public UpdateMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var movie=_dbContext.Movies.FirstOrDefault(m=>m.Id==MovieId);
            if (movie is null)
            {
                throw new InvalidOperationException("Film Bulunamadi.");
            }

            var isGenreNull = _dbContext.Genres.Any(g => g.Id == Model.GenreId);
            if (!isGenreNull)
            {
                throw new InvalidOperationException("Film Turu Bulunamadi.");
            }

            var isDirectorNull = _dbContext.Directors.Any(g => g.Id == Model.DirectorId);
            if (!isDirectorNull)
            {
                throw new InvalidOperationException("Yonetmen Bulunamadi.");
            }

            var hasTitle = _dbContext.Movies.Any(m => m.Title.ToLower().Replace(" ", "") == Model.Title.ToLower().Replace(" ", "")
            && m.DirectorId == Model.DirectorId
            && m.Id != MovieId);
            if (hasTitle)
            {
                throw new InvalidOperationException("Bu İsimde Film Bulunmaktadır.");
            }

            movie.Title = !string.IsNullOrEmpty(Model.Title) ? Model.Title : movie.Title;
            movie.DirectorId= !int.IsNegative(Model.DirectorId) ? Model.DirectorId : movie.DirectorId;
            movie.GenreId = !int.IsNegative(Model.GenreId) ? Model.GenreId : movie.GenreId;
            movie.Price = decimal.IsPositive(Model.Price) ? Model.Price : movie.Price;
            movie.IsActive = Model.IsActive;

            _dbContext.SaveChanges();
        }

    }
}
