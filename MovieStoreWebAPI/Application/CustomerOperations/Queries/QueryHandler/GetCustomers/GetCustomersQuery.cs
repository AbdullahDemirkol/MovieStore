using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.CustomerOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.CustomerOperations.Queries.QueryHandler.GetCustomers
{

    public class GetCustomersQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCustomersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<CustomerViewModel> Handle()
        {
            var customers = _dbContext.Customers
                .Include(c => c.Orders)
                .Include(c => c.FavoriteGenre)
                .OrderBy(c => c.Id)
                .ToList();
            List<CustomerViewModel> viewModels = _mapper.Map<List<CustomerViewModel>>(customers);
            return viewModels;
        }
    }
}
