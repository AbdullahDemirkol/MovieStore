using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler
{
    public class AddFavoriteGenreCommand
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }

        private readonly IMovieStoreDbContext _dbContext;

        public AddFavoriteGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == CustomerId);
            if (customer is null)
            {
                throw new InvalidOperationException("Müsteri Bulunamadi.");
            }
            
            var genre = _dbContext.Genres.FirstOrDefault(c => c.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Film Turu Bulunamadi.");
            }

            customer.FavoriteGenre.Add(genre);

            _dbContext.SaveChanges();
        }
    }
}
