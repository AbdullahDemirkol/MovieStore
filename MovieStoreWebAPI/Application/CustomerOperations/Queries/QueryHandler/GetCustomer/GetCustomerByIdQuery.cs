using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.CustomerOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.CustomerOperations.Queries.QueryHandler.GetCustomer
{
    public class GetCustomerByIdQuery
    {
        public int CustomerId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCustomerByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        internal CustomerViewModel Handle()
        {
            var customer = _dbContext.Customers
                .Include(c => c.Orders)
                .Include(c => c.FavoriteGenre)
                .FirstOrDefault(c => c.Id == CustomerId);
            if (customer is null)
            {
                throw new InvalidOperationException("Musteri Bulunamadi.");
            }

            CustomerViewModel viewModel=_mapper.Map<CustomerViewModel>(customer);
            return viewModel;
        }
    }
}
