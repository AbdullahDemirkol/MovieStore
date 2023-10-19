using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.OrderOperations.Queries.QueryHandler.GetOrder
{
    public class GetOrderByIdQuery
    {
        public int OrderId { get; set; }

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public OrderViewModel Handle()
        {
            var order=_dbContext.Orders
                .Include(o=>o.Movie)
                .Include(o=>o.Customer)
                .FirstOrDefault(o=>o.Id==OrderId);
            if (order is null)
            {
                throw new InvalidOperationException("Siparis Bulunamadi.");
            }
            OrderViewModel viewModel=_mapper.Map<OrderViewModel>(order);
            return viewModel;
        }
    }
}
