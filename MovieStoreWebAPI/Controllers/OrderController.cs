using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreWebAPI.Application.OrderOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.OrderOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.OrderOperations.Commands.Validator;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryHandler.GetOrder;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryHandler.GetOrders;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryViewModel;
using MovieStoreWebAPI.Application.OrderOperations.Queries.Validator;
using MovieStoreWebAPI.DataAccess.Abstract;

namespace MovieStoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderModel newOrder)
        {
            CreateOrderCommand command=new CreateOrderCommand(_dbContext,_mapper);
            command.Model = newOrder;

            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetOrders()
        {
            GetOrdersQuery query=new GetOrdersQuery(_dbContext, _mapper);
            List<OrderViewModel> orders = query.Handle();
            return Ok(orders);
        }
        [HttpGet("{customerId}")]
        public IActionResult GetOrdersByCustomerId(int customerId)
        {
            GetOrdersByCustomerIdQuery query = new GetOrdersByCustomerIdQuery(_dbContext, _mapper);
            query.CustomerId = customerId;

            GetOrderByCustomerIdQueryValidator validator = new GetOrderByCustomerIdQueryValidator();
            validator.ValidateAndThrow(query);

            List<OrderViewModel> viewModels = query.Handle();
            return Ok(viewModels);
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            GetOrderByIdQuery query = new GetOrderByIdQuery(_dbContext, _mapper);
            query.OrderId = id;

            GetOrderByIdQueryValidator validator = new GetOrderByIdQueryValidator();
            validator.ValidateAndThrow(query);

            OrderViewModel viewModel = query.Handle();
            return Ok(viewModel);
        }
    }
}
