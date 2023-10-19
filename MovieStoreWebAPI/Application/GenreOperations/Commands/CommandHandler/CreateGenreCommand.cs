using AutoMapper;
using MovieStoreWebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Application.GenreOperations.Commands.CommandHandler
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre=_dbContext.Genres.FirstOrDefault(g=>g.Name==Model.Name);
            if (genre is not null)
            {
                throw new InvalidOperationException("Film Türü Bulunmaktadır.");
            }
            
            genre = _mapper.Map<Genre>(Model);
            
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }
    }
}
