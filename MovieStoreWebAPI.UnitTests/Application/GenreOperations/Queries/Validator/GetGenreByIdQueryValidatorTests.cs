using FluentAssertions;
using MovieStoreWebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;
using MovieStoreWebAPI.Application.GenreOperations.Queries.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.Application.GenreOperations.Queries.Validator
{
    public class GetGenreByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int genreId)
        {
            //Arrange(Hazırlık)
            GetGenreByIdQuery command = new GetGenreByIdQuery(null, null);
            command.GenreId = genreId;

            //Act (Çalıştırma)
            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            var result = validator.Validate(command);


            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
