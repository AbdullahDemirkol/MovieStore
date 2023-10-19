using AutoMapper;
using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre
{
    public class GetGenreByIdQuery
    {
        public int GenreId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public GenreViewModel Handle()
        {
            var genre=_dbContext.Genres.FirstOrDefault(g=>g.IsActive && g.Id==GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Film Türü Bulunamadı.");
            }
            GenreViewModel viewModel=_mapper.Map<GenreViewModel>(genre);
            return viewModel;
        }
    }
}
