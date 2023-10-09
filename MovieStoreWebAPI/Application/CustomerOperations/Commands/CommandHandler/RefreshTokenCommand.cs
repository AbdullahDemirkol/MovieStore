using MovieStoreWebAPI.Application.TokenOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.TokenOperations.Commands.RequestCommanModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IMovieStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.RefreshToken == RefreshToken && c.RefreshTokenExpireDate > DateTime.Now);
            if (customer is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(customer);
                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.ExpireDate.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Geçerli Bir Refresh Token Bulunamadı");
            }
        }
    }
}
