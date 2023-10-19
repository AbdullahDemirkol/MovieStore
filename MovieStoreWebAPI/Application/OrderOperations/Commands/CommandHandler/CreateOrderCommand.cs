using AutoMapper;
using MovieStoreWebAPI.Application.OrderOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Application.OrderOperations.Commands.CommandHandler
{
    public class CreateOrderCommand
    {
        public CreateOrderModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public void Handle()
        {
            bool hasMovie = _dbContext.Movies.Any(m => m.Id == Model.MovieId && m.IsActive);
            if (!hasMovie)
            {
                throw new InvalidOperationException("Film Bulunamadi.");
            }

            bool hasCustomer=_dbContext.Customers.Any(c=>c.Id==Model.CustomerId && c.IsActive);
            if (!hasCustomer)
            {
                throw new InvalidOperationException("Musteri Bulunamadi.");
            }

            var order = _mapper.Map<Order>(Model);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }
    }
}
