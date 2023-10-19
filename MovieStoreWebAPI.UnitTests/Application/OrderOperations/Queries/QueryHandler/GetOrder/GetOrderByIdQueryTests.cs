using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovie;
using MovieStoreWebAPI.Application.OrderOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.OrderOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryHandler.GetOrder;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.OrderOperations.Queries.QueryHandler.GetOrder
{
    public class GetOrderByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Order_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            var movieId = _dbContext.Movies.FirstOrDefault().Id;
            var customerId = _dbContext.Customers.FirstOrDefault().Id;
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            command.Model = new CreateOrderModel()
            {
                MovieId = movieId,
                CustomerId = customerId
            };
            command.Handle();

            var orderId = _dbContext.Orders.FirstOrDefault(o => o.MovieId == movieId && o.CustomerId == customerId).Id;
            GetOrderByIdQuery query = new GetOrderByIdQuery(_dbContext, _mapper);
            query.OrderId = orderId;

            //Act (Çalıştırma)
            var order = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert (Doğrulama)
            var registeredOrder = _dbContext.Orders.Include(o=>o.Movie).Include(o=>o.Customer).FirstOrDefault(o => o.Id == orderId);
            order.CustomerName.Should().NotBeNull(registeredOrder.Customer.Name + " " + registeredOrder.Customer.Surname);
            order.MovieName.Should().NotBeNull(registeredOrder.Movie.Title);
        }
        [Fact]
        public void WhenInvalidOrderIdIsGiven_InvalidOperationExceptions_ShouldBeError()
        {

            //Assert (Doğrulama)
            GetOrderByIdQuery query = new GetOrderByIdQuery(_dbContext, _mapper);
            query.OrderId = 999999999;

            //Act & Arrange (Çalıştırma-Hazırlık)
            var movie = FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Siparis Bulunamadi.");
        }
    }
}
