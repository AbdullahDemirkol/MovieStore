using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler
{
    public class DeleteCustomerCommand
    {
        public int CustomerId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteCustomerCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == CustomerId);
            if (customer is null)
            {
                throw new InvalidOperationException("Müşteri Bulunamadi.");
            }

            customer.IsActive = false;

            _dbContext.SaveChanges();
        }

    }
}
