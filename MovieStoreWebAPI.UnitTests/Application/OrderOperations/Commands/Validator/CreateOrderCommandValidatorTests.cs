using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStoreWebAPI.Application.OrderOperations.Commands.CommandHandler;
using MovieStoreWebAPI.Application.OrderOperations.Commands.RequestCommandModel;
using MovieStoreWebAPI.Application.OrderOperations.Commands.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.OrderOperations.Commands.Validator
{
    public class CreateOrderCommandValidatorTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]

        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int movieId, int customerId)
        {
            //Arrange(Hazırlık)
            CreateOrderCommand command = new CreateOrderCommand(null, null);
            command.Model = new CreateOrderModel()
            {
                MovieId = movieId,
                CustomerId = customerId
            };

            //Act (Çalıştırma)
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
