using AutoMapper;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.Entity.Concrete;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler
{
    public class CreateCustomerCommand
    {
        public CreateCustomerModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCustomerCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Email == Model.Email);
            if (customer is not null)
            {
                throw new InvalidOperationException("Kullanıcı Zaten Mevcut.");
            }
            customer = _mapper.Map<Customer>(Model);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

    }
}
