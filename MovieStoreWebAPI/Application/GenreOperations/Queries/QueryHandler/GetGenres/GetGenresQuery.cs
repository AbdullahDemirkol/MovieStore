using AutoMapper;
using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenresQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<GenreViewModel> Handle()
        {
            var genres=_dbContext.Genres.OrderBy(g=> g.Id).Where(g=>g.IsActive).ToList();
            List<GenreViewModel> viewModel=_mapper.Map<List<GenreViewModel>>(genres);
            return viewModel;
        }
    }
}
