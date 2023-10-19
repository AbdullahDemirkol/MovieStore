using AutoMapper;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.TokenOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.TokenOperations.Commands.RequestCommanModel;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IMovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var customer=_dbContext.Customers.FirstOrDefault(c=>c.Email==Model.Email && c.Password==Model.Password && c.IsActive);
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
                throw new InvalidOperationException("Kullanıcı Adı yada Şifre Hatalı!");
            }
        }
    }
}
