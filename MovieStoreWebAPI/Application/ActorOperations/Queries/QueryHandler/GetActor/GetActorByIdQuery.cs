using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.ActorOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.ActorOperations.Queries.QueryHandler.GetActor
{
    public class GetActorByIdQuery
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetActorByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public ActorViewModel Handle()
        {
            var actor=_dbContext.Actors
                .Include(a=>a.Movies).ThenInclude(m=>m.Movie)
                .FirstOrDefault(a=>a.Id==ActorId);
            if (actor is null)
            {
                throw new InvalidOperationException("Aktör Bulunamadı.");
            }

            ActorViewModel viewModel = _mapper.Map<ActorViewModel>(actor);
            return viewModel;
            
        }

    }
}
