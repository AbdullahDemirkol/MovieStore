using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.OrderOperations.Queries.QueryHandler.GetOrders
{
    public class GetOrdersByCustomerIdQuery
    {
        public int CustomerId { get; set; }

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrdersByCustomerIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public List<OrderViewModel> Handle()
        {
            var orders = _dbContext.Orders
                .Include(o => o.Movie)
                .Include(o => o.Customer)
                .Where(o => o.CustomerId == CustomerId)
                .OrderBy(o => o.CustomerId)
                .ToList();
            List<OrderViewModel> viewModels = _mapper.Map<List<OrderViewModel>>(orders);
            return viewModels;
        }
    }
}
