using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.DirectorOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.DirectorOperations.Queries.QueryHandler.GetDirector
{
    public class GetDirectorByIdQuery
    {
        public int DirectorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public DirectorViewModel Handle()
        {
            var director = _dbContext.Directors.Include(d=>d.Movies).FirstOrDefault(d=>d.Id==DirectorId);
            if (director is null)
            {
                throw new InvalidOperationException("Yönetmen Bulunamadı.");
            }
            DirectorViewModel viewModel=_mapper.Map<DirectorViewModel>(director);
            return viewModel;
        }
    }
}
