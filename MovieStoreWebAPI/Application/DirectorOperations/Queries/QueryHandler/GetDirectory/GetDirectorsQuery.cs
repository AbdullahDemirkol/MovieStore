using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.DirectorOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.DirectorOperations.Queries.QueryHandler.GetDirectory
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<DirectorViewModel> Handle()
        {
            var directors = _dbContext.Directors
                .Include(d => d.Movies)
                .OrderBy(d=>d.Id)
                .ToList();
            List<DirectorViewModel> viewModels = _mapper.Map<List<DirectorViewModel>>(directors);
            return viewModels;
        }

    }
}
