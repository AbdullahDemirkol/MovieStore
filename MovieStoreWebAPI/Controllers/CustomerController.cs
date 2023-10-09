using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.CustomerOperations.Commands.Validator;
using MovieStoreWebAPI.Application.TokenOperations.Commands.RequestCommanModel;
using MovieStoreWebAPI.DataAccess.Concrete;

namespace MovieStoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomerController(MovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        //Register
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext,_mapper);
            command.Model = newCustomer;
            
            CreateCustomerCommandValidator validator=new CreateCustomerCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        //Login
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel newToken)
        {
            CreateTokenCommand command=new CreateTokenCommand(_dbContext, _mapper,_configuration);
            command.Model = newToken;

            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            validator.ValidateAndThrow(command);

            var token = command.Handle();
            return token;
        }

        [HttpGet("refresh/token")]
        public ActionResult<Token> RefreshToken([FromQuery] string refreshToken)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_dbContext, _configuration);
            command.RefreshToken = refreshToken;

            RefreshTokenCommandValidator validator = new RefreshTokenCommandValidator();
            validator.ValidateAndThrow(command);

            var resultToken= command.Handle();
            return resultToken;
        }
    }
}
