using AutoMapper;
using FluentAssertions;
using MovieStoreWebAPI.Application.OrderOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.OrderOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.Entity.Concrete;
using MovieStoreWebAPI.UnitTests.TestsSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.OrderOperations.Commands.CommandHandler
{
    public class CreateOrderCommandTests:IClassFixture<CommonTestFixture>
    {

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Order_ShouldBeCreated()
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

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var order = _dbContext.Orders.FirstOrDefault(am => am.MovieId == movieId && am.CustomerId == customerId);
            order.Should().NotBeNull();
        }
        [Fact]
        public void WhenNonCustomerIdIsGiven_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            var movieId = _dbContext.Movies.FirstOrDefault().Id;
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            command.Model = new CreateOrderModel()
            {
                MovieId = movieId,
                CustomerId = 99999999
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Musteri Bulunamadi.");
        }
        [Fact]
        public void WhenNonMovieIdIsGiven_InvalidOperationException_ShouldBeError()
        {

            //Arrange(Hazırlık)
            var customerId = _dbContext.Customers.FirstOrDefault().Id;
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            command.Model = new CreateOrderModel()
            {
                MovieId = 99999999,
                CustomerId = customerId
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Film Bulunamadi.");
        }
    }
}
