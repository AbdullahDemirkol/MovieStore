using FluentAssertions;
using MovieStoreWebAPI.Application.MovieOperations.Queries.QueryHandler.GetMovie;
using MovieStoreWebAPI.Application.MovieOperations.Queries.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.MovieOperations.Queries.Validator
{
    public class GetMovieByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidMovieIdsAreGive_InvalidOperationException_ShouldBeReturned(int movieId)
        {
            //Assert (Doğrulama)
            GetMovieByIdQuery query = new GetMovieByIdQuery(null, null);
            query.MovieId= movieId;

            //Act (Çalıştırma)
            GetMovieByIdQueryValidator validator = new GetMovieByIdQueryValidator();
            var result=validator.Validate(query);

            //Arrange (Hazırlık)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
