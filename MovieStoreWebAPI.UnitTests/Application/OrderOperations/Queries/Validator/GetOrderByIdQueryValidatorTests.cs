using FluentAssertions;
using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;
using MovieStoreWebAPI.Application.GenreOperations.Queries.Validator;
using MovieStoreWebAPI.Application.OrderOperations.Queries.QueryHandler.GetOrder;
using MovieStoreWebAPI.Application.OrderOperations.Queries.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.OrderOperations.Queries.Validator
{
    public class GetOrderByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int orderId)
        {
            //Arrange(Hazırlık)
            GetOrderByIdQuery command = new GetOrderByIdQuery(null, null);
            command.OrderId = orderId;

            //Act (Çalıştırma)
            GetOrderByIdQueryValidator validator = new GetOrderByIdQueryValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
