using AutoMapper;
using MovieStoreWebAPI.Application.MovieOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.DataAccess.Concrete;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Application.MovieOperations.Commands.CommandHandler
{
    public class CreateMovieCommand
    {
        public CreateMovieModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.FirstOrDefault(m => m.Title == Model.Title && m.DirectorId == Model.DirectorId);
            if(movie is not null)
            {
                throw new InvalidOperationException("Bu Isim Ve Yönetmene Ait Film Bulunmaktadır.");
            }
            var hasGenre=_dbContext.Genres.Any(g=>g.Id == Model.GenreId);
            if (!hasGenre)
            {
                throw new InvalidOperationException("Gecerli Bir Film Turu Bulunamadi.");
            }

            movie = _mapper.Map<Movie>(Model);
            
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
        }
    }
}
